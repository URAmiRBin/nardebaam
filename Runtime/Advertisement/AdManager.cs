using System.Collections.Generic;
using UnityEngine;
using System;

public class AdManager {
    AdService bannerProvider;
    public readonly int maxTries = 3;
    List<AdService> services;

    List<AdService> _rewardedServices, _interstitialServices;
    int _rewardedNextServiceIndex, _interestialNextServiceIndex;
    bool _bypassForceAds;
    AdIterationType _iterationType;
    
    public bool IsRewardedReady {
        get {
            for (int i = 0; i < _rewardedServices.Count; i++) {
                if (_rewardedServices[i].IsRewardedReady) return true;
            }
            return false;
        }
    }

    public AdManager(AdConfig config) {
        _interstitialServices = new List<AdService>();
        _rewardedServices = new List<AdService>();

        _bypassForceAds = ES3.Load<bool>(SaveKeys.NOADS, false);
        BuildServices(config);
        InitializeAds(config.isTestBuild);
    }

    void BuildServices(AdConfig adConfig) {
        _iterationType = adConfig.iterationType;
        services = new List<AdService>();
        foreach(AdServiceConfig adServiceConfig in adConfig.adServices) {
            switch (adServiceConfig.network) {
                case AdNetwork.Admob:
                    var admobAdService = new AdmobAdService(adServiceConfig.units);
                    services.Add(admobAdService);
                    break;
                case AdNetwork.Unity:
                    var unityAdsService = new UnityAdService(adServiceConfig.units);
                    unityAdsService.gameId = adServiceConfig.appID;
                    services.Add(unityAdsService);
                    break;
            }
        }
    }

    // TODO: Initialize later if internet is connected in the middle of the session
    public void InitializeAds(bool testMode) {
        for (int i = 0; i < services.Count; i++) {
            if (!services[i].excludeInterstital) _interstitialServices.Add(services[i]);
            if (!services[i].excludeRewarded) _rewardedServices.Add(services[i]);
            services[i].Initialize(testMode);
        }

        if (services.Count != 0) bannerProvider = services[0];
    }

    public void ShowInterstitial(Action success = null, Action fail = null) {
        if (_bypassForceAds) return;
        switch (_iterationType) {
            case AdIterationType.Loop:
                ShowInterstitialLoop(success, fail);
                break;
            case AdIterationType.Absolute:
                ShowInterstitialAbsolute(success, fail);
                break;
        }
    }

    // FIXME: Too many duplicates
    void ShowInterstitialLoop(Action success = null, Action fail = null) {
        for (int i = 0; i < _interstitialServices.Count; i++) {
            int clampedIndex = ListUtils.ClampListIndex(i + _interestialNextServiceIndex, _interstitialServices.Count);
            if (_interstitialServices[clampedIndex].IsInterstitialReady) {
                _interstitialServices[clampedIndex].ShowInterstitial(success, fail);
                _interestialNextServiceIndex = clampedIndex + 1;
                return;
            }
        }
        fail?.Invoke();
    }

    void ShowInterstitialAbsolute(Action success = null, Action fail = null) {
        for (int i = 0; i < _interstitialServices.Count; i++) {
            if (_interstitialServices[i].IsInterstitialReady) {
                _interstitialServices[i].ShowInterstitial(success, fail);
                return;
            }
        }
        fail?.Invoke();
    }

    public void ShowRewarded(Action success, Action fail) {
        switch (_iterationType) {
            case AdIterationType.Loop:
                ShowRewardedLoop(success, fail);
                break;
            case AdIterationType.Absolute:
                ShowRewardedAbsolute(success, fail);
                break;
        }
    }

    void ShowRewardedLoop(Action success, Action fail) {
        for (int i = 0; i < _rewardedServices.Count; i++) {
            int clampedIndex = ListUtils.ClampListIndex(i + _rewardedNextServiceIndex, _rewardedServices.Count);
            if (_rewardedServices[clampedIndex].IsRewardedReady) {
                _rewardedServices[clampedIndex].ShowRewarded(success, fail);
                _rewardedNextServiceIndex = clampedIndex + 1;
                return;
            }
        }
        fail?.Invoke();
    }

    void ShowRewardedAbsolute(Action success, Action fail) {
        for (int i = 0; i < _rewardedServices.Count; i++) {
            if (_rewardedServices[i].IsRewardedReady) {
                _rewardedServices[i].ShowRewarded(success, fail);
                return;
            }
        }
        fail?.Invoke();
    }

    public void ShowBanner() {
        if (_bypassForceAds) return;
        bannerProvider?.ShowBanner();
    }
    public void DestroyBanner() => bannerProvider?.HideBanner();

    public void BypassForceAds() {
        _bypassForceAds = true;
        DestroyBanner();
    }

}
