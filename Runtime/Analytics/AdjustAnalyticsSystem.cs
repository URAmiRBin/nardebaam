using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.adjust.sdk;

public class AdjustAnalyticsSystem : AnalyticsSystem {
    protected override void SendLevelStartEvent(int level) {
        AdjustEvent adjustEvent = new AdjustEvent("LevelStart");
        adjustEvent.addCallbackParameter("level", level.ToString());
        Adjust.trackEvent(adjustEvent);
    }

    protected override void SendLevelWinEvent(int level) {
        AdjustEvent adjustEvent = new AdjustEvent("LevelWin");
        adjustEvent.addCallbackParameter("level", level.ToString());
        Adjust.trackEvent(adjustEvent);
    }

    protected override void SendLevelLoseEvent(int level) {
        AdjustEvent adjustEvent = new AdjustEvent("LevelLose");
        adjustEvent.addCallbackParameter("level", level.ToString());
        Adjust.trackEvent(adjustEvent);
    }

    protected override void SendCustomEvent(string type, float value) {
        AdjustEvent adjustEvent = new AdjustEvent(type);
        adjustEvent.addCallbackParameter("key", value.ToString());
        Adjust.trackEvent(adjustEvent);
    }

    protected override void SendCurrencySpendEvent(int value) {
        AdjustEvent adjustEvent = new AdjustEvent("SpendCurrency");
        adjustEvent.addCallbackParameter("value", value.ToString());
        Adjust.trackEvent(adjustEvent);
    }

    protected override void SendCurrencyEarnEvent(int value) {
        AdjustEvent adjustEvent = new AdjustEvent("EarnCurrency");
        adjustEvent.addCallbackParameter("value", value.ToString());
        Adjust.trackEvent(adjustEvent);
    }

    protected override void SendAdFailEvent() {
        AdjustEvent adjustEvent = new AdjustEvent("AdFail");
        Adjust.trackEvent(adjustEvent);
    }

    protected override void SendAdShowEvent() {
        AdjustEvent adjustEvent = new AdjustEvent("AdShow");
        Adjust.trackEvent(adjustEvent);
    }

}
