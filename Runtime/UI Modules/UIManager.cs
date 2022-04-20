using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] GameStates defaultState;
    [SerializeField] UIMaps maps;
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] Popup popupInstance;
    [SerializeField] UIElements elements;
    [SerializeField] GameObject progressionGameObject;

    static Popup popup;
    GameStates currentState;
    UIElement currentPanel;
    public UIElements Elements { get => elements; }

    public void Initialize(UIConfig config) {
        elements.settingsPanel.privacyButton?.onClick.AddListener(() => Application.OpenURL(config.privacyURL));
        elements.fingerTutorial.Initialize(config.tutorialFingerType);
        elements.startGame.SetCallback(() => UpdateState(GameStates.Gameplay));
        elements.settingsButton.onClick.AddListener(() => elements.settingsPanel.Open());
        elements.closeSettingsButton.onClick.AddListener(() => elements.settingsPanel.Close());
        elements.agreementPopup.agreeButton.onClick.AddListener(AgreeToConditions);

        elements.shopButton.onClick.AddListener(() => elements.shopPanel.Open());
        elements.selectorButton.onClick.AddListener(() => elements.selectorPanel.Open());
        elements.closeShopPanel.onClick.AddListener(() => elements.shopPanel.Close());
        elements.closeSelectorPanel.onClick.AddListener(() => elements.selectorPanel.Close());

        switch (config.progressIndicatorType) {
            case ProgressIndicatorType.Boss:
                elements.levelProgressIndicator = progressionGameObject.AddComponent<BossLevelProgressIndicator>().Initialize(config);
                break;
            case ProgressIndicatorType.Theme:
                elements.levelProgressIndicator = progressionGameObject.AddComponent<ThemeLevelProgressIndicator>().Initialize(config);
                break;
            default:
                elements.levelProgressIndicator = progressionGameObject.AddComponent<LevelProgressIndicator>().Initialize(config);
                break;
        }

        SetLevelText(ES3.Load(SaveKeys.LEVEL, 1));

        elements.agreementPopup.Initialize(config.agreementsText);
        elements.AddListenerToAllButtons(() => Runner.AudioPlayer.PlaySFX(Runner.SoundEffects.clickUI));
    }

    void Awake() {
        UpdateState(defaultState);
        popup =  Instantiate(popupInstance, transform);
        DontDestroyOnLoad(this);
    }

    void OnEnable() {
        NardeboonEvents.UIEvents.onStateChange += UpdateState;
        NardeboonEvents.GameLogicEvents.onLevelWin += HandleLevelWin;
        NardeboonEvents.GameLogicEvents.onLevelLose += HandleLevelLose;
    }

    void OnDisable() {
        NardeboonEvents.UIEvents.onStateChange -= UpdateState;
        NardeboonEvents.GameLogicEvents.onLevelWin -= HandleLevelWin;
        NardeboonEvents.GameLogicEvents.onLevelLose -= HandleLevelLose;
    }
    
    void UpdateState(GameStates state) {
        if (state == currentState) return;
        currentPanel?.Close();
        currentState = state;
        try {
            currentPanel = maps[state.ToString()] as UIElement;
            currentPanel.Open();
            backgroundPanel.SetActive(currentPanel.hasBackground);
        } catch (NullReferenceException) {
            Debug.LogWarning("State " + state.ToString() + " is not defined.");
        }
    }

    public void UpdateCoin(string amount) {
        elements.coin.text = amount;
    }

    void HandleLevelWin(int level) {
        SetLevelText(level + 1);
        NardeboonEvents.UIEvents.onStateChange?.Invoke(GameStates.Win);
    }

    void HandleLevelLose(int level) {
        NardeboonEvents.UIEvents.onStateChange?.Invoke(GameStates.Lose);
    }

    void SetLevelText(int level) {
        elements.levelProgressIndicator.SetLevel(level);
        foreach(Text levelText in elements.levelTexts) {
            levelText.text = "LEVEL " + (level).ToString();
        }
    }

    void LevelStart() => UpdateState(GameStates.MainMenu);

    // FIXME: Don't use static, Jamasb told
    public static void ShowPopup(string message, Action yesCallback = null, Action noCallback = null, string yesText = "YES", string noText = "NO") {
        // TODO: Connect to animation system
        if (popup.IsActive) ClosePopup();
        popup.AssemblePopup(message, yesCallback, noCallback, yesText, noText);
        popup.Open();
    }

    void AgreeToConditions() {
        PlayerPrefs.SetInt(PlayerPrefKeys.AGREED, 1);
        elements.agreementPopup.Close();
        UpdateState(GameStates.MainMenu);
    }

    public void ShowAgreements() => elements.agreementPopup.Open();

    public static void ClosePopup() {
        popup.Close();
    }
}
