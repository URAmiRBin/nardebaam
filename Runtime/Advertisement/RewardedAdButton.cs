using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdButton : MonoBehaviour {
    Button _button;

    void Awake() => _button = GetComponent<Button>();
    
    async void Start() => await RewardedReady(new CancellationToken());

    async Task RewardedReady(CancellationToken ct) {
        var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        try {
            var linkedCt = linkedCts.Token;
            var adReady = WaitForRewardedAd(linkedCt);
            await adReady;
            linkedCts.Cancel();
        } finally {
            linkedCts.Dispose();
            _button.interactable = true;
        }
    }

    async Task WaitForRewardedAd(CancellationToken ct) {
        while (!ct.IsCancellationRequested && !Runner.AdManager.IsRewardedReady) {
            await Task.Yield();
        }
    }
}
