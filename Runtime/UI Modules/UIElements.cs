using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class UIElements {
    public Button retryButton;
    public Button reviveButton;
    public Button nextLevelButton;
    public Button settingsButton;
    public Button closeSettingsButton;
    public Text[] levelTexts;
    [HideInInspector] public LevelProgressIndicator levelProgressIndicator;
    public ButtonDown startGame;
    public Text coin;
    public SettingsPanel settingsPanel;
    public FingerTutorial fingerTutorial;
    public ProgressLoadingScreen loadingScreen;
    public AgreementPopup agreementPopup;
    public Panel shopPanel;
    public Panel selectorPanel;
    public Button shopButton;
    public Button selectorButton;
    public Button closeShopPanel;
    public Button closeSelectorPanel;

    public void AddListenerToAllButtons(UnityAction callback) {
        // TODO: Store buttons in an array
        retryButton.onClick.AddListener(callback);
    }
}
