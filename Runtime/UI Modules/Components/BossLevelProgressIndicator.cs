using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelProgressIndicator : LevelProgressIndicator {
    Sprite _bossSpritePassed, _bossSpriteNotPassed;

    public override LevelProgressIndicator Initialize(UIConfig config) {
        base.Initialize(config);
        _bossSpritePassed = config.bossSpritePassed;
        _bossSpriteNotPassed = config.bossSpriteNotPassed;
        levelTexts[cap - 1].enabled = false;
        return this;
    }

    public override void SetLevel(int level) {
        base.SetLevel(level);
        progressImages[cap - 1].sprite = level != 0 && level % cap == 0 ? _bossSpritePassed : _bossSpriteNotPassed;
    }
}
