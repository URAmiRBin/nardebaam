using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader {
    private static readonly string itemsPath = "Items/";
    public static T LoadData<T>(string path) where T : UnityEngine.Object {
        return Resources.Load<T>(path);
    }

    public static GameItem LoadItem(string name) => LoadData<GameItem>(itemsPath + name);

    public static GameItem[] LoadAllItems() => Resources.LoadAll<GameItem>(itemsPath);
}

public static class InventoryManager {
    static GameItem[] items;
    
    public static void InitializeItems() {
        items = ResourceLoader.LoadAllItems();
        for (int i = 0; i < items.Length; i++) {
            ItemCallbackSetter.SetCallback(items[i]);
        }
    }
}