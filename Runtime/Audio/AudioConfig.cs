using System;
using UnityEngine;

[Serializable]
public class AudioConfig {
    [Range(0.0f, 1.0f)] public float MasterSound;
    public MusicLibrary musics;
    public SFXLibrary soundEffects;
}

[Serializable]
public class SoundClass {
    public AudioClip Clip;

    [Range(0f, 2f)]
    public float Volume = 1f;

    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;

    [Range(-1, 1)]
    public float SoundChanel = 0;

    public bool Loop;
}

[Serializable]
public class SFXLibrary {
    public SoundClass gameover;
    public SoundClass clickUI;
    public SoundClass openUI;
    public SoundClass win;
    public SoundClass coinDling;
}

[Serializable]
public class MusicLibrary {
    public SoundClass[] menu;
}