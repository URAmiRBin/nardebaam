using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorItemUI : ShopItemUI {
    public override void FillData(InventoryItem item, Action callback) {
        _priceContainer.SetActive(false);
        _item = item;
        _image.sprite = item.Sprite;
        _button.onClick.AddListener(() => callback?.Invoke());
        _button.onClick.AddListener(Click);
    }
    
    protected override void Click() {
        ChangeState(true);
        _item.Config.Use();
    }
}
