using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIParticle {
    Confetti,
}

[System.Serializable]
public class UIParticleMap : ReflectableClass {
    public ParticleSystem Confetti;
}

public class UIParticleManager : MonoBehaviour {
    [SerializeField] GameObject viewImage;
    [SerializeField] UIParticleMap particles;

    void OnEnable() => NardeboonEvents.UIEvents.onStateChange += ShowStateParticles;

    public void ShowUIParticle(UIParticle particleType) {
        ParticleSystem particle = particles[particleType.ToString()] as ParticleSystem;
        particle.Play();
    } 

    void ShowStateParticles(GameStates state) {
        switch(state) {
            case GameStates.Win:
                viewImage.SetActive(true);
                ShowUIParticle(UIParticle.Confetti);
                break;
            default:
                viewImage.SetActive(false);
                break;
        }
    }
}
