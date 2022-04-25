using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Medrick.Nardeboon {
    public class DelayedButton : MonoBehaviour {
        [SerializeField] GameObject _cancelButton;
        [SerializeField] float _fadeDelayInSeconds;
        
        void OnEnable() {
            _cancelButton.SetActive(false);
            StartCoroutine(AsyncHelper.WaitAndDoCouroutine(() => _cancelButton.SetActive(true), _fadeDelayInSeconds));
        }
    }
}