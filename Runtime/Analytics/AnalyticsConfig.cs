using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnalyticsConfig {
    public bool useAnalytics;

    [Header("GameAnalytics")]
    [ConditionalHide("useAnalytics", true)]
    public bool useGameAnalytics;
    
    [ConditionalHide("useGameAnalytics", true)]
    public GameObject gameAnalytics;
    
    [ConditionalHide("useGameAnalytics", true)]
    public string gameAnalyticsGameKey;

    [ConditionalHide("useGameAnalytics", true)]
    public string gameAnalyticsSecretKey;
    
    [ConditionalHide("useAnalytics", true)]

    [Header("Adjust")]
    public bool useAdjust;
    [ConditionalHide("useAdjust", true)]
    public GameObject adjustPrefab;
    [ConditionalHide("useAdjust", true)]
    public string adjustToken;
}
