using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Facebook.Unity;
using UnityEngine;

namespace Medrick.Nardeboon
{
    public class FacebookAnalytics : AnalyticsSystem
    {
        protected override void SendLevelStartEvent(int level)
        {
            FB.Init();
        }

        protected override void SendLevelWinEvent(int level)
        {
            throw new System.NotImplementedException();
        }

        protected override void SendLevelLoseEvent(int level)
        {
            throw new System.NotImplementedException();
        }

        protected override void SendCustomEvent(string type, float value)
        {
            throw new System.NotImplementedException();
        }

        protected override void SendCurrencySpendEvent(int value)
        {
            throw new System.NotImplementedException();
        }

        protected override void SendCurrencyEarnEvent(int value)
        {
            throw new System.NotImplementedException();
        }

        protected override void SendAdFailEvent()
        {
            throw new System.NotImplementedException();
        }

        protected override void SendAdShowEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}