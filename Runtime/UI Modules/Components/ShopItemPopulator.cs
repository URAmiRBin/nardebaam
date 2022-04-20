using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPopulator : MonoBehaviour {
    [SerializeField] ShopItemUI defaultItemPrefab;
    [SerializeField] Transform contentsParent;
    [SerializeField] InventoryItem[] shopItems;
    ShopItemUI[] uiItems;

    void Awake() {
        uiItems = new ShopItemUI[shopItems.Length];
    }

    // TODO: ynamically fill data from save data
    void Start() {
        for(int i = 0; i < shopItems.Length; i++) {
            uiItems[i] = Instantiate(defaultItemPrefab, contentsParent);
            uiItems[i].FillData(shopItems[i], DeselectAll);
        }
    }

    void DeselectAll() {
        for(int i = 0; i < uiItems.Length; i++) {
            uiItems[i].ChangeState(false);
        }
    }
}
