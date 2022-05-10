# Nardeboon: Unity Mobile Modular Framework

Nardeboon is a comprehensive package that implements all the needed services for your mobile games. With Nardeboon you can save time and do **NOT** install other SDKs twice and focus on your game and productivity.

**Table of Contents**

- [Services](#services)
    - [Advertisements](#advertisements)
        - [Admob](#admob)
        - [UnityAds](#unityads)
    - [Analytics](#analytics)
    - [Save](#save)
    - [Audio](#audio)
    - [Haptics](#haptics)
    - [Economy](#economy)
    - [UI](#ui)
- [Install](#install)
- [Use](#use)
- [Suggestions](#suggestions)


# Services
Nardeboon implements multiple services and depends on external packages and developement kits.

## Advertisements
For now Nardeboon supports Admob and UnityAds. You can even have both of these ad networks in your game.

To intialize AdManager you have to pass `Adconfig` to it, which has three fields:

`isTestBuild` enables test mode for all the ad services and is used for debug and internal builds.

`iterationType` is used when you have multiple ad networks. 

In **Absolute** mode, AdManager always prioritize the network that comes first and only uses the next ad network when the first one is not ready.

In **Loop** mode, AdManager loops through ad services and do not prioritize over any ad network. If one ad network is not available at the time, AdManager skips over that one for that iteration.

![Advertisement Config](/images/adconfig.png)

### Admob
Admob is Google's mobile ads and comes with External Dependency Manager by default.

### UnityAds
UnityAds is an in-engine package and handles advertisements natively from the engine.

> Note that duplicate ad services is not tested, hence it's not supported. The editor logs an error if duplicate ad networks is found but does not do anything further than that, so the developers should be aware of this.

> UnityAds does not pause the game when the ad is loaded and being shown to the player, so the developers should handle that (which will be handled by the package in the next iterations)

To show any ads, use `Runner.AdManager` and then call the `ShowInterstitial` or `ShowRewarded` or `ShowBanner` to show an ad if available. you can pass success and fail callbacks to handle what happens if an ad is shown or could not be shown. some of these callbacks should change UI states and will be handled within the framework in the next iterations.

## Analytics

To monitor the events of the game, we can use analytics packages to provide the data so we can analyze them and make our games better.

Nardeboon uses [GameAnalytics](https://github.com/googleads/googleads-mobile-unity) and [Adjust](https://github.com/adjust/unity_sdk) for analytics purposes, but the developers have the option to choose between these services, if they desire any.

![Analytics config](/images/analytics-config.png)

To use the analytics system, all the developers have to do is to invoke one of these events:

- `NardeboonEvents.GameLogicEvents.onLevelStart`
- `NardeboonEvents.GameLogicEvents.onLevelWin`
- `NardeboonEvents.GameLogicEvents.onLevelLose`
- `NardeboonEvents.onCustomEvent`
- `NardeboonEvents.EconomyEvents.onCurrencyEarn`
- `NardeboonEvents.EconomyEvents.onCurrencySpend`
- `NardeboonEvents.AdEvents.onAdFail`
- `NardeboonEvents.AdEvents.onAdShow`

Note that these events can be used in other cases, for example onLevelStart is called whenever the player starts a level, with this event, the UI system as well as the analytics system will react and change the panel.

## Save
Nardeboon uses [EasySave3](https://docs.moodkie.com/product/easy-save-3/) which is a paid package in order to handle the saves.

In order to save you can use `ES3.Save` and to load use `ES3.Load`. Read the [documentations](https://docs.moodkie.com/product/easy-save-3/) for more details.


## Audio
A basic audio manager system is implemented in the framework

![Audio Config](/images/audio.png)

To use music or sfx, you can call `Runner.AudioPlayer.PlayMusic` or `Runner.AudioPlayer.PlaySFX` and feed in the audio you want. you can use the preset sound effects which is stored in `Runner.SoundEffects`.

## Haptics

The vibration calls can be called from the core game logic to give more agency to the developers or from the game events within the framework. There are two basic vibration call presets (short and long vibration) which can be set from the package interface.

![Haptics](/images/vibration.png)

To use vibration you can use `Runner.VibrationManager.ShortVibrate` or `Runner.VibrationManager.LongVibrate`. you can also use the custom `Vibrate` method in VibrationManager and have the vibration go for your desired duration.

> Note that the framework prevents multiple vibration calls and cancels the previous vibration to call for another vibration first.

## Economy

Nardeboon comes with an internal economy system in which you can define your own items in it and use the built-in inventory system or connect it to the shop.

The items can be defined in `Resources/Items` folder and the InventorySystem will fetch all the items at the startup.

![Item](/images/game-item.png)

## UI

# Install

# Use

# Suggestions