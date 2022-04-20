using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIElement : MonoBehaviour {
    public bool hasBackground;
    public bool playSoundEffect;
    public bool IsActive {get; protected set;}

    public virtual void Open() {
        IsActive = true;
        foreach(Transform child in transform) child.gameObject.SetActive(true);
        if (playSoundEffect) Runner.AudioPlayer.PlaySFX(Runner.SoundEffects.openUI);
    }
    public virtual void Close() {
        IsActive = false;
        foreach(Transform child in transform) child.gameObject.SetActive(false);
    }
}