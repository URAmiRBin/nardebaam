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

In absolute mode, AdManager always prioritize the network that comes first and only uses the next ad network when the first one is not ready.

In Loop mode, AdManager loops through ad services and do not prioritize over any ad network.

### Admob
Admob is Google's mobile ads and comes with External Dependency Manager

### UnityAds

## Analytics

## Save

## Audio

## Haptics

## Economy

## UI

# Install

# Use

# Suggestions