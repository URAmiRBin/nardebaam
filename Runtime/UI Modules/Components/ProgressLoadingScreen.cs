using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLoadingScreen : MonoBehaviour {
    [SerializeField] Image fillImage;

    public void SetProgress(float percent) {
        fillImage.fillAmount = percent;
        if (percent >= 1f) FinishProgress();
    }

    public void StartProgress() {
        foreach(Transform child in transform)
            child.gameObject.SetActive(true);
        fillImage.fillAmount = 0;
    }

    public void FinishProgress() {
        foreach(Transform child in transform)
            child.gameObject.SetActive(false);
    }
}
