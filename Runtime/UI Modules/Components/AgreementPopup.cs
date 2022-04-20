using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgreementPopup : UIElement {
    public Text agreementsText;
    public Toggle agreedToggle;
    public Button agreeButton;
    
    public void Initialize(string message) {
        agreementsText.text = message;
    }

    void Awake() {
        agreedToggle.onValueChanged.AddListener(SwitchAgreeToggle);
        agreeButton.interactable = false;
    }

    void SwitchAgreeToggle(bool value) {
        agreeButton.interactable = value;
    }
}
