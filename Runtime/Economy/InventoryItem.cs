using UnityEngine;

public interface Collectable {
    void Collect(int amount = 1);
    void Use(int amount = 1);
}

[System.Serializable]
public class InventoryItem : Collectable {
    [SerializeField] GameItem _item;
    [SerializeField] int amount;
    InventoryItemStorageData _storageData;

    public int Amount { get => amount; }
    public string Name { get => _item.name; }
    public Sprite Sprite { get => _item.image; }
    public int Price { get => _item.cost * (IsConsumable ? amount : 1); }
    public bool IsConsumable { get => _item.consumable; }
    public InventoryItemStorageData StorageData => _storageData;
    public GameItem Config { get => _item; }

    public InventoryItem(GameItem item, int amount = 0) {
        _item = item;
        this.amount = amount;
        _storageData = new InventoryItemStorageData(Name, Amount);
    }

    public void Collect(int amount = 1) {
        this.amount += amount;
        _storageData.Amount = Amount;
    }

    public void Use(int amount = 1) {
        if (amount > this.amount) throw new System.InvalidOperationException();
        this.amount -= amount;
        _item.Use();
        _storageData.Amount = Amount;
    }
}

[System.Serializable]
public enum GameItemValue {
    Common, Rare, Epic
}

[System.Serializable]
public class InventoryItemStorageData {
    [ES3Serializable] string _name;
    [ES3Serializable] int _amount;

    public int Amount { 
        get => _amount;
        set => _amount = value; 
    }

    public string Name { get => _name; }

    public InventoryItemStorageData(string name, int amount) {
        _name = name;
        _amount = amount;
    }

    public InventoryItemStorageData() {
        _name = "";
        _amount = 0;
    }
}