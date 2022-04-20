using GoogleMobileAds.Api;
using System;

public class AdmobAdService : AdService {
    BannerView _banner;
    InterstitialAd _interstitial;
    RewardedAd _rewarded;
    
    public override bool IsRewardedReady { get => _rewarded != null && _rewarded.IsLoaded(); }
    public override bool IsInterstitialReady { get => _interstitial != null && _interstitial.IsLoaded(); }
    public override bool IsBannerReady { get => _banner != null; }

    public AdmobAdService(AdUnits units) : base(units) {}
    
    
    public override void Initialize(bool testMode) {
        if (testMode) {
            bannerUnit = "ca-app-pub-3940256099942544/6300978111";
            interstitialUnit = "ca-app-pub-3940256099942544/1033173712";
            RewardedUnit = "ca-app-pub-3940256099942544/5224354917";
        }
        MobileAds.Initialize(initStatus =>
        {
            RequestBanner(AdSize.Banner);
            RequestRewarded();
            RequestInterstitial();
            _banner.OnAdLoaded += HandleOnAdLoaded;
            _banner.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        });
    }

    public override void ShowRewarded(Action success, Action fail) {
        onAdFail = fail;
        onAdSuccess = success;
        this._rewarded.Show();
        RequestRewarded();
    }

    public override void ShowInterstitial(Action success, Action fail) {
        onAdFail = fail;
        onAdSuccess = success;
        this._interstitial.Show();
        RequestInterstitial();
    }

    public override void ShowBanner() => _banner?.Show();
    public override void HideBanner() => _banner?.Hide();

    public void RequestRewarded() {
        this._rewarded = new RewardedAd(RewardedUnit);
        this._rewarded.OnAdFailedToShow += OnAdFailedCallback;
        this._rewarded.OnUserEarnedReward += OnRewardedCallback;
        AdRequest request = new AdRequest.Builder().Build();
        this._rewarded.LoadAd(request);
    }

    private void OnRewardedCallback(object sender, EventArgs desc) {
        RequestRewarded();
        if (onAdSuccess != null) onAdSuccess();
    }

    private void OnAdFailedCallback(object sender, AdErrorEventArgs desc) {
        RequestRewarded();
        if (onAdSuccess != null) onAdFail();
    }


    private void RequestBanner(AdSize adSize) {
        _banner = new BannerView(bannerUnit, adSize, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        _banner.LoadAd(request);
        _banner.Hide();
    }

    private void RequestInterstitial() {
        this._interstitial = new InterstitialAd(interstitialUnit);
        AdRequest request = new AdRequest.Builder().Build();
        this._interstitial.LoadAd(request);
        _interstitial.OnAdClosed += OnInterestialDone;
        _interstitial.OnAdFailedToShow += OnInterestialFailed;
    }


    private void HandleOnAdLoaded(object sender, EventArgs args) => _loadTries = 0;

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        if (_loadTries <= Runner.AdManager.maxTries) {
            RequestBanner(AdSize.Banner);
            _loadTries++;
        }
            
    }

    private void OnInterestialDone(object sender, EventArgs args) {
        RequestInterstitial();
        if (onAdSuccess != null) onAdSuccess();
    }

    private void OnInterestialFailed(object sender, EventArgs args) {
        RequestInterstitial();
        if (onAdSuccess != null) onAdSuccess();
    }

    private void OnDisable() {
        if (_banner == null) return;
        _banner.OnAdLoaded -= HandleOnAdLoaded;
        _banner.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
    }
}
