using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    GameItem currency;
    public List<InventoryItem> _items = new List<InventoryItem>();
    List<InventoryItemStorageData> _storageItems;
    
    public int Wallet {
        get {
            if (_items[0] == null) Debug.LogError("Make sure to initialize Inventory System!");
            return _items[0].Amount;
        } 
    }
    public string MainCurrency {
        get {
            if (_items[0] == null) Debug.LogError("Make sure to initialize Inventory System!");
            return _items[0].Name;
        }
    }

    public void Initialize(GameItem mainCurrency) {
        currency = mainCurrency;
        SetupInventoryFromSave();
        Runner.UIManager.UpdateCoin(Wallet.ToString());
    }

    void SetupInventoryFromSave() {
        _storageItems = ES3.Load(SaveKeys.INVENTORY, new List<InventoryItemStorageData>());
        
        for(int i = 0; i < _storageItems.Count; i++) {
            // FIXME: Get this from inventory manager not resource loader
            GameItem item = ResourceLoader.LoadItem(_storageItems[i].Name);
            if (item == null) continue;
            _items.Add(new InventoryItem(item, _storageItems[i].Amount));
        }
        
        if (_items.Count == 0) AddToInventory(new InventoryItem(currency));
    }

    public void AddToInventory(InventoryItem item) {
        if (item == null || item.Price < 0) return;
        int index = _items.FindIndex(i => i.Name == item.Name);
        if (index == -1) {
            _items.Add(item);
            _storageItems.Add(new InventoryItemStorageData(item.Name, item.Amount));
        }
        else {
            _items[index].Collect(item.Amount);
            _storageItems[index] = _items[index].StorageData;
        }
        ES3.Save(SaveKeys.INVENTORY, _storageItems);
    }

    public bool HasItemWithName(string name) => _items.Exists(x => x.Name == name);

    public void AddToWallet(int amount) {
        _items[0].Collect(amount);
        NardeboonEvents.EconomyEvents.onCurrencyEarn?.Invoke(amount);
        Runner.UIManager.UpdateCoin(Wallet.ToString());
        _storageItems[0] = _items[0].StorageData;
        ES3.Save(SaveKeys.INVENTORY, _storageItems);
    }

    public void SpendFromWallet(int amount) {
        _items[0].Use(amount);
        NardeboonEvents.EconomyEvents.onCurrencySpend?.Invoke(amount);
        Runner.UIManager.UpdateCoin(Wallet.ToString());
        _storageItems[0] = _items[0].StorageData;
        ES3.Save(SaveKeys.INVENTORY, _storageItems);
    }

    public void BuyItem(InventoryItem item) {
        try {
            SpendFromWallet(item.Price);
            AddToInventory(item);
        } catch (System.InvalidOperationException e) {
            throw e;
        }
    }
}