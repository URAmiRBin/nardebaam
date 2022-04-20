public abstract class AnalyticsSystem {

    public virtual void Initialize() {
        NardeboonEvents.GameLogicEvents.onLevelStart += SendLevelStartEvent;
        NardeboonEvents.GameLogicEvents.onLevelWin += SendLevelWinEvent;
        NardeboonEvents.GameLogicEvents.onLevelLose += SendLevelLoseEvent;
        NardeboonEvents.onCustomEvent += SendCustomEvent;
        NardeboonEvents.EconomyEvents.onCurrencyEarn += SendCurrencyEarnEvent;
        NardeboonEvents.EconomyEvents.onCurrencySpend += SendCurrencySpendEvent;
        NardeboonEvents.AdEvents.onAdFail += SendAdFailEvent;
        NardeboonEvents.AdEvents.onAdShow += SendAdShowEvent;
    }

    public virtual void Destroy() {
        NardeboonEvents.GameLogicEvents.onLevelStart -= SendLevelStartEvent;
        NardeboonEvents.GameLogicEvents.onLevelWin -= SendLevelWinEvent;
        NardeboonEvents.GameLogicEvents.onLevelLose -= SendLevelLoseEvent;
        NardeboonEvents.onCustomEvent -= SendCustomEvent;
        NardeboonEvents.EconomyEvents.onCurrencyEarn -= SendCurrencyEarnEvent;
        NardeboonEvents.EconomyEvents.onCurrencySpend -= SendCurrencySpendEvent;
        NardeboonEvents.AdEvents.onAdFail -= SendAdFailEvent;
        NardeboonEvents.AdEvents.onAdShow -= SendAdShowEvent;
    }

    protected abstract void SendLevelStartEvent(int level);
    protected abstract void SendLevelWinEvent(int level);
    protected abstract void SendLevelLoseEvent(int level);
    protected abstract void SendCustomEvent(string type, float value);
    protected abstract void SendCurrencySpendEvent(int value);
    protected abstract void SendCurrencyEarnEvent(int value);
    protected abstract void SendAdFailEvent();
    protected abstract void SendAdShowEvent();
}