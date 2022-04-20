using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class GameItem : ScriptableObject {
    public new string name;
    [Multiline]
    public string description;
    public bool consumable;
    public Sprite image;
    public GameItemValue rarity;
    public int cost;
    public UnityEvent artifacts;

    // TODO: Handle multiple uses
    public void Use(int amount = 1) => artifacts?.Invoke();
}