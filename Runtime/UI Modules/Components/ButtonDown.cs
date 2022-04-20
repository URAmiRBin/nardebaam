using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDown : Button, IPointerDownHandler {
    [SerializeField] Action callback;

    public void SetCallback(Action callback) {
        this.callback = callback;
    }

    public override void OnPointerDown(PointerEventData pointerEventData) {
        base.OnPointerDown(pointerEventData);
        callback?.Invoke();
    }
}