using UnityEngine;

public class MonoBehaviourSingletion<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance {
        get {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<T>();
            if (_instance != null) return _instance;
            GameObject go = new GameObject(typeof(T).Name);
            _instance = go.AddComponent<T>();
            return _instance;
        }
    }
}