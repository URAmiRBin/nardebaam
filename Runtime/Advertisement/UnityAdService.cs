using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Medrick.Nardeboon {
    public class UnityAdService : AdService, IUnityAdsListener {
        public string gameId;

        public override bool IsRewardedReady => Advertisement.IsReady();
        public override bool IsInterstitialReady => Advertisement.IsReady();
        public override bool IsBannerReady => Advertisement.IsReady();

        public UnityAdService(AdUnits units) : base(units) {}

        public override void Initialize(bool testMode) {
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }

        public override void ShowInterstitial(Action success, Action fail) {
            onAdSuccess = success;
            onAdFail = fail;
            Advertisement.Show(interstitialUnit);
        }

        public override void ShowRewarded(Action success, Action fail) {
            onAdSuccess = success;
            onAdFail = fail;
            Advertisement.Show(RewardedUnit);
        }

        public override void ShowBanner() => Advertisement.Show(bannerUnit);

        public override void HideBanner() => Advertisement.Banner.Hide();

        public void OnUnityAdsDidFinish(string unitId, ShowResult showResult) {
            switch (showResult) {
                case ShowResult.Failed:
                    onAdFail?.Invoke();
                    break;
                case ShowResult.Finished:
                    onAdSuccess?.Invoke();
                    break;
                case ShowResult.Skipped:
                    onAdFail?.Invoke();
                    break;
            }
        }

        public void OnUnityAdsReady(string placementId) {}

        public void OnUnityAdsDidError (string message) {}

        public void OnUnityAdsDidStart(string placementId) {}

    }
}
