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

Nardeboon uses **GameAnalytics** and **Adjust** for analytics purposes, but the developers have the option to choose between these services, if they desire any.

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

## Audio

## Haptics

## Economy

## UI

# Install

# Use

# Suggestions