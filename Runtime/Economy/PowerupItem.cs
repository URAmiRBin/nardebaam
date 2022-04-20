using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Powerup Item", menuName = "ScriptableObjects/Item", order = 1)]
public class PowerupItem : ScriptableObject {
    public new string name;
    [Multiline]
    public string description;
    public Sprite image;
    public int[] cost;
    public UnityEvent<int> artifacts;

    // TODO: Handle multiple uses
    public void Use(int level = 1) => artifacts?.Invoke(level);
}
