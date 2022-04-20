using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour {
    [SerializeField] protected Image _image;
    [SerializeField] protected Button _button;
    [SerializeField] protected GameObject _priceContainer;
    [SerializeField] protected Text _priceText;
    protected InventoryItem _item;
    protected int indexInMenu;

    protected bool CanClickOnItem {
        get => _item.IsConsumable || !Runner.InventorySystem.HasItemWithName(_item.Name);
    }

    // FIXME: Action? for real??
    public virtual void FillData(InventoryItem item, Action callback) {
        _item = item;
        _image.sprite = item.Sprite;
        _priceText.text = item.Price.ToString();
        _button.onClick.AddListener(Click);
        _priceContainer.SetActive(CanClickOnItem);
    }

    protected virtual void Click() {
        if (!CanClickOnItem) return;
        try {
            Runner.InventorySystem.BuyItem(new InventoryItem(_item.Config, _item.Amount));
            _priceContainer.SetActive(CanClickOnItem);
        } catch (System.InvalidOperationException) {
            UIManager.ShowPopup("Not enough money", UIManager.ClosePopup, yesText: "OK");
        }
    }

    public void ChangeState(bool isHighlight) {
        GetComponent<Image>().color = isHighlight ? Color.grey : Color.white;
    }
}
