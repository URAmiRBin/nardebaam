using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Facebook.Unity;
using UnityEngine;

namespace Medrick.Nardeboon
{
    public class FacebookAnalytics : AnalyticsSystem
    {
        protected override void SendLevelStartEvent(int level) { }

        protected override void SendLevelWinEvent(int level) { }
        
        protected override void SendLevelLoseEvent(int level) { }
        
        protected override void SendCustomEvent(string type, float value) { }
        
        protected override void SendCurrencySpendEvent(int value) { }
        
        protected override void SendCurrencyEarnEvent(int value) { }
        
        protected override void SendAdFailEvent() { }
        
        protected override void SendAdShowEvent() { }
    }
}