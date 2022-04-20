using System;
using UnityEngine;

[Serializable]
public class AdConfig {
    [HideInInspector] public bool isTestBuild;
    public AdIterationType iterationType;
    public AdServiceConfig[] adServices;
}

[Serializable]
public class AdUnits : ReflectableClass {
    public string banner, interstitial, rewarded;
}

[Serializable]
public enum AdNetwork {
    Admob, Unity,
}

[Serializable]
public enum AdIterationType {
    Absolute, Loop
}

[Serializable]
public class AdServiceConfig {
    public AdNetwork network;
    public string appID;
    public AdUnits units;
}