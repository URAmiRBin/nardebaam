using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : UIElement {
    [SerializeField] Text popupMessage;
    [SerializeField] Button yesButton, noButton;
    [SerializeField] Text yesText, noText;

    public void AssemblePopup(string message, Action yesCallback, Action noCallback, string yesText, string noText) { 
        popupMessage.text = message;
        if (yesCallback == null) {
            yesButton.onClick.AddListener(Close);
            noButton.gameObject.SetActive(false);
        } else if (noCallback == null) {
            yesButton.onClick.AddListener(() => yesCallback());
            noButton.gameObject.SetActive(false);
        } else {
            yesButton.onClick.AddListener(() => yesCallback());
            noButton.onClick.AddListener(() => noCallback());
        }
    }
}
