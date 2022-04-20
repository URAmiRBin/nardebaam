using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class ItemCallbackSetter {
    public static UnityAction GetItemCallback(string name) {
        switch (name) {
            case "Skeleton":
                return () => {
                    Debug.Log("Used Skeleton");
                };
            case "Red Ball":
                return () => {
                    Debug.Log("You've got red balls");
                };
            default:
                return null;
        }
    }

    public static void SetCallback(GameItem item) {
        UnityAction callback = GetItemCallback(item.name);
        if (callback != null) item.artifacts.AddListener(GetItemCallback(item.name));
    }
}
