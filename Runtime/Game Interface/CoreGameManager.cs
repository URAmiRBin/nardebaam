using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreEvents {
    public static Action onCurrentLevelWin;
    public static Action onCurrentLevelLose;
}

public class CoreGameManager : MonoBehaviour, ICore {
    int _level;

    void Awake() {
        _level = ES3.Load<int>(SaveKeys.LEVEL, 1);
    }

    void OnEnable() {
        CoreEvents.onCurrentLevelWin += WinLevel;
        CoreEvents.onCurrentLevelLose += LoseLevel;
    }

    void OnDisable() {
        CoreEvents.onCurrentLevelWin -= WinLevel;
        CoreEvents.onCurrentLevelLose -= LoseLevel;
    }

    public void Initialize() {
        HookButtons();
    }

    void HookButtons() {
        Runner.UIElements.reviveButton.onClick.AddListener(Revive);
        Runner.UIElements.nextLevelButton.onClick.AddListener(() => StartLevel(++_level));
        Runner.UIElements.retryButton.onClick.AddListener(ReplayLevel);   
        Runner.UIElements.settingsButton.onClick.AddListener(FreezeGame);
        Runner.UIElements.closeSettingsButton.onClick.AddListener(UnFreezeGame); 
    }

    public void StartGame() {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.AGREED, 0) == 1) {
            NardeboonEvents.UIEvents.onStateChange(GameStates.MainMenu);
        } else {
            Runner.UIManager.ShowAgreements();
        }
    }

    public void ExitGame() {}

    public void ReplayLevel() {
        Runner.AdManager.ShowInterstitial(RestartLevel, RestartLevel);
    }

    public void WinLevel() {
        NardeboonEvents.GameLogicEvents.onLevelWin?.Invoke(_level);
        ES3.Save(SaveKeys.LEVEL, _level + 1);
        Runner.AudioPlayer.PlaySFX(Runner.SoundEffects.win);  
    }

    public void LoseLevel() {
        NardeboonEvents.GameLogicEvents.onLevelLose?.Invoke(_level);
        Runner.AudioPlayer.PlaySFX(Runner.SoundEffects.gameover);
    }

    public void Revive() {}
    public void FreezeGame() {}
    public void UnFreezeGame() {}
    public void StartLevel(int level) {
        SceneManager.LoadScene(1);
        NardeboonEvents.UIEvents.onStateChange?.Invoke(GameStates.MainMenu);
    }

    public void RestartLevel() => StartLevel(_level);    

    public void GetLevelReward() {

    }
}
