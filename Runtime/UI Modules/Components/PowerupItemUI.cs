using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupState {
    Purchasable, WatchAd, MaxedOut
}

public class PowerupItemUI : MonoBehaviour {
    [SerializeField] GameObject priceContainer, watchAdContainer, maxedContainer;
    [SerializeField] Text priceText;
    [SerializeField] Text levelText;
    [SerializeField] Button powerupButton;
    [SerializeField] Image powerupImage;
    int maxLevel;
    PowerupState _currentState;
    int _level;
    PowerupItem _item;

    void Awake() => powerupButton.onClick.AddListener(Upgrade);

    void Start() => UpdateState();

    void FillData(PowerupItem item) {
        _level = ES3.Load<int>(SaveKeys.LEVEL, 1);
        _item = item;
        maxLevel = _item.cost.Length;
        powerupImage.sprite = _item.image;
    }

    // TODO: Refresh state on panel open

    PowerupState GetState() {
        if (_level == maxLevel) return PowerupState.MaxedOut; 
        // if (PlayerInventory.Instance.CanSpend(_config.cost[_level - 1])) return PowerupState.Purchasable;
        else return PowerupState.WatchAd;
    }

    public void UpdateState() {
        priceText.text = _item.cost[_level - 1].ToString();
        levelText.text = "LVL " + _level.ToString();        
        _currentState = GetState();
        DisableAll();
        switch (_currentState) {
            case PowerupState.Purchasable:
                priceContainer.SetActive(true);
                break;
            case PowerupState.WatchAd:
                watchAdContainer.SetActive(true);
                break;
            case PowerupState.MaxedOut:
                maxedContainer.SetActive(true);
                break;
        }
    }

    void DisableAll() {
        priceContainer.SetActive(false);
        watchAdContainer.SetActive(false);
        maxedContainer.SetActive(false);
    }

    void Upgrade() {
        switch (_currentState) {
            case PowerupState.Purchasable:
                NardeboonEvents.EconomyEvents.onCurrencySpend(_item.cost[_level - 1]);
                _level++;
                UpdateState();
                _item.Use(_level);
                break;
            case PowerupState.WatchAd:
                Runner.AdManager.ShowRewarded(
                    () => {
                        // TODO: Get this from curve
                        _level++;
                        UpdateState();
                        _item.Use(_level);  
                    },
                    () => UIManager.ShowPopup("No internet")
                );
                break;
            case PowerupState.MaxedOut:
                break;
        }
    }
}
