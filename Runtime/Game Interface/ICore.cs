using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICore {
    void Initialize();
    void StartGame();
    void ExitGame();
    void ReplayLevel();
    void WinLevel();
    void LoseLevel();
    void Revive();
    void FreezeGame();
    void UnFreezeGame();
    void StartLevel(int level);
    void RestartLevel();
    void GetLevelReward();
}
