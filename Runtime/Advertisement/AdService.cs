using UnityEngine;
using System;

public abstract class AdService {
    protected int _loadTries;

    public string bannerUnit;
    public bool excludeInterstital;
    public string interstitialUnit;

    public bool excludeRewarded;
    public string RewardedUnit;

    public abstract bool IsRewardedReady { get; }
    public abstract bool IsInterstitialReady { get; }
    public abstract bool IsBannerReady { get; }
    
    public Action onAdSuccess, onAdFail;

    public abstract void Initialize(bool testMode);
    public abstract void ShowRewarded(Action success, Action fail);
    public abstract void ShowInterstitial(Action success, Action fail);
    public abstract void ShowBanner();
    public abstract void HideBanner();
    public AdService(AdUnits units) {
        bannerUnit = units.banner;
        interstitialUnit = units.interstitial;
        RewardedUnit = units.rewarded;
    }
}

public enum AdResult { Success, Fail }