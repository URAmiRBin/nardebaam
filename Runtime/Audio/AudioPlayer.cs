using System;
using UnityEngine;


public class AudioPlayer {
    private float _masterSound;
    private AudioSource _musicPlayer;
    private AudioSource _sfxPlayer;
    private bool _sound;

    public bool SoundStatus {
        get => _sound;
        private set {
            _sound = value;
            PlayerPrefs.SetInt(PlayerPrefKeys.SOUND, _sound ? 1 : 0);
            _musicPlayer.mute = _sound;
        }
    }

    public AudioPlayer(float masterSound, AudioSource musicPlayer, AudioSource sfxPlayer) {
        _masterSound = masterSound;
        
        _musicPlayer = musicPlayer;
        musicPlayer.spatialBlend = 0;
        musicPlayer.loop = true;
        musicPlayer.volume = 1;

        _sfxPlayer = sfxPlayer;

        sfxPlayer.spatialBlend = 0;
        sfxPlayer.loop = false;
        sfxPlayer.playOnAwake = false;
        sfxPlayer.volume = 1;

        NardeboonEvents.UIEvents.onSoundSetEvent += SetSoundStatus;
    }

    ~AudioPlayer() {
        NardeboonEvents.UIEvents.onSoundSetEvent += SetSoundStatus;
    }

    public void PlayMusic(SoundClass clip) {
        _musicPlayer.clip = clip.Clip;
        _musicPlayer.mute = !_sound;
        _musicPlayer.volume = clip.Volume * _masterSound;
        _musicPlayer.pitch = clip.Pitch;

        _musicPlayer.loop = clip.Loop;

        _musicPlayer.Play();
    }

    public void PlaySFX(SoundClass clip, float pitch = 1) {
        _sfxPlayer.clip = clip.Clip;
        _sfxPlayer.mute = !_sound;
        _sfxPlayer.volume = clip.Volume * _masterSound;
        _sfxPlayer.pitch = pitch;
        _sfxPlayer.panStereo = clip.SoundChanel;

        _sfxPlayer.PlayOneShot(clip.Clip, _sound ? 1 : 0);
    }

    public void PlayMusic(AudioClip clip, bool loop = false) {
        _musicPlayer.clip = clip;
        _musicPlayer.mute = !_sound;
        _musicPlayer.volume = _masterSound;
        _musicPlayer.loop = loop;
        _musicPlayer.Play();
    }

    public void PlaySFX(AudioClip clip, float pitch = 1) {
        _sfxPlayer.clip = clip;
        _sfxPlayer.mute = !_sound;
        _sfxPlayer.volume = _masterSound;
        _sfxPlayer.pitch = pitch;
        _sfxPlayer.PlayOneShot(clip, _sound ? 1 : 0);
    }

    void SetSoundStatus(bool state) => SoundStatus = state;
}
