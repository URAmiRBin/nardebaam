using UnityEngine;

public class VibrationManager {
    private bool _log;
    private long _shortVibrationTimeInMilliseconds = 50;
    private long _longVibrationTimeInMilliseconds = 150;
    private bool _vibration = false;

    public VibrationManager(long shortVibrationDurationInMilliseconds, long longVibrationDurationInMilliseconds, bool log) {
        _shortVibrationTimeInMilliseconds = shortVibrationDurationInMilliseconds;
        _longVibrationTimeInMilliseconds = longVibrationDurationInMilliseconds;
        _log = log;
        NardeboonEvents.UIEvents.onVibrationSetEvent += SetVibrationStatus;
    }

    ~VibrationManager() {
        NardeboonEvents.UIEvents.onVibrationSetEvent -= SetVibrationStatus;
    }

    public bool VibrationStatus {
        get => _vibration;
        private set {
            _vibration = value;
            PlayerPrefs.SetInt(PlayerPrefKeys.VIBRATION, _vibration ? 1 : 0);
        }
    }
 
    void SetVibrationStatus(bool status) => VibrationStatus = status;

    public void Vibrate(long vibrationDuration) {
        if (!_vibration) return;
        if (_log) Debug.Log("VIBRATING FOR " + vibrationDuration + " milliseconds.");
        // Cancel previous vibration to avoid annoying multi vibrations
        Vibration.Cancel();
        Vibration.Vibrate(vibrationDuration);
    }

    public void ShortVibrate() => Vibrate(_shortVibrationTimeInMilliseconds);
    public void LongVibrate() => Vibrate(_longVibrationTimeInMilliseconds);
}
