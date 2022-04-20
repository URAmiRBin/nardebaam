using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.adjust.sdk;
using UnityEngine.EventSystems;

public class Runner : MonoBehaviour {
    [SerializeField] bool isProductionBuild;
    [SerializeField] bool makeEventSystem;
    [SerializeField] CoreGameManager gameManagerPrefab;
    ICore _gameManager;

    [SerializeField] AnalyticsConfig analyticsConfig;

    [SerializeField] AdConfig adConfig;
    public static AdManager AdManager;

    [SerializeField] UIConfig uiConfig;
    public static UIManager UIManager;
    public static UIElements UIElements {
        get => UIManager?.Elements;
    }

    [Header("Economy")]
    [SerializeField] GameItem mainCurrency;
    public static Inventory InventorySystem;

    [Header("Save")]
    [SerializeField] GameObject easySaveManagerPrefab;

    ProgressLoadingScreen loadingPanel;

    [Header("Vibration")]
    [SerializeField] bool logVibrationInEditor;
    [SerializeField] long shortVibrationDurationInMilliseconds;
    [SerializeField] long longVibrationDurationInMilliseconds;
    public static VibrationManager VibrationManager;

    [Header("Audio")]
    [SerializeField] AudioConfig audioConfig;
    public static AudioPlayer AudioPlayer;
    public static MusicLibrary Musics;
    public static SFXLibrary SoundEffects;

    // Dependencies
    public static AnalyticsSystem GameAnalytics {get; private set;}
    public static AnalyticsSystem AdjustAnalytics {get; private set;}
    
    void Awake() {
        // FIXME: Start the loading process before setting up services not after it
        DontDestroyOnLoad(this);
        _gameManager = Instantiate(gameManagerPrefab, transform);
        ConfigPreprocess();
        StartCoroutine(SetupServices());
        StartCoroutine(LoadGameScene());
    }

    void ConfigPreprocess() {
        // TODO: Remove parts according to used packages
        uiConfig.agreementsText = uiConfig.agreementsText.Replace("****", Application.productName);
    }

    IEnumerator SetupServices() {
        SetupUIManager();
        SetupEventSystem();
        SetupInventorySystem();
        SetupAnalytics();
        SetupAdvertisement();
        setupAudioAndVibration();
        _gameManager.Initialize();
        yield break;
    }

    void SetupUIManager() {
        UIManager = Instantiate(uiConfig.uiManagerPrefab);
        UIManager.Initialize(uiConfig);
        loadingPanel = UIElements.loadingScreen;
        loadingPanel.SetProgress(0);
    }

    void SetupEventSystem() {
        if (makeEventSystem) {
            GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            eventSystem.transform.SetParent(transform);
        }
    }

    void SetupInventorySystem() {
        InventoryManager.InitializeItems();
        InventorySystem = new GameObject("Inventory System").AddComponent<Inventory>();
        InventorySystem.transform.SetParent(transform);
        InventorySystem.Initialize(mainCurrency);
    }

    void SetupAnalytics() {
        if (!analyticsConfig.useAnalytics) return;
        if (analyticsConfig.useGameAnalytics) {
            try {
                // Set GA settings
                GameAnalyticsSDK.Setup.Settings gaSettings = Resources.Load<GameAnalyticsSDK.Setup.Settings>("GameAnalytics/Settings");

                // TODO: Multiplatform support
                if (gaSettings.Platforms.Count == 0) {
                    gaSettings.InfoLogEditor = false;
                    gaSettings.AddPlatform(RuntimePlatform.Android);
                    gaSettings.UpdateGameKey(0, analyticsConfig.gameAnalyticsGameKey);
                    gaSettings.UpdateSecretKey(0, analyticsConfig.gameAnalyticsSecretKey);
                    gaSettings.Build[0] = Application.version;    
                }

                if (gaSettings.ResourceCurrencies.Count == 0) gaSettings.ResourceCurrencies.Add(InventorySystem.MainCurrency);
                if (gaSettings.ResourceItemTypes.Count == 0) gaSettings.ResourceItemTypes.Add("Game Item");

                // Initialize GA
                Instantiate(analyticsConfig.gameAnalytics).transform.SetParent(transform);
                GameAnalytics = new GameAnalyticsSystem();
                GameAnalytics.Initialize();
            } catch (Exception) {
                Debug.LogError("Can not initialize GameAnalytics!");
            }
        }

        if (analyticsConfig.useAdjust) {
            try {
                if (analyticsConfig.adjustPrefab != null) {
                    Instantiate(analyticsConfig.adjustPrefab, transform);
                    AdjustAnalytics = new AdjustAnalyticsSystem();
                    AdjustEnvironment adjustEnv = isProductionBuild ? AdjustEnvironment.Production : AdjustEnvironment.Sandbox;
                    AdjustConfig adjustConfig = new AdjustConfig(analyticsConfig.adjustToken, adjustEnv);
                    adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
                    Adjust.start(adjustConfig);
                    AdjustAnalytics.Initialize();
                }
            } catch (Exception) {
                Debug.LogError("Can not initialize Adjust!");
            }
        }
    }

    void SetupAdvertisement() {
        try {
            adConfig.isTestBuild = !isProductionBuild;
            AdManager = new AdManager(adConfig);
        } catch (Exception) {
            Debug.LogError("Can not initialize ad services!");
        }
    }

    void setupAudioAndVibration() {
        VibrationManager = new VibrationManager(shortVibrationDurationInMilliseconds, longVibrationDurationInMilliseconds, logVibrationInEditor);

        GameObject audioManagerObject = new GameObject("Audio Manager", typeof(AudioSource), typeof(AudioSource));
        Musics = audioConfig.musics;
        SoundEffects = audioConfig.soundEffects;
        audioManagerObject.transform.SetParent(transform);
        AudioSource[] audioSources = audioManagerObject.GetComponents<AudioSource>();
        AudioPlayer = new AudioPlayer(audioConfig.MasterSound, audioSources[0], audioSources[1]);
    }

    IEnumerator LoadGameScene() {
        if (SceneManager.sceneCountInBuildSettings <= 1) {
            Debug.LogError("Core game scene is not added to the build settings!");
            yield break;
        }
        AsyncOperation gameLoadOperation = SceneManager.LoadSceneAsync(1);
        
        loadingPanel.StartProgress();
        while (!gameLoadOperation.isDone) {
            loadingPanel.SetProgress(.9f + gameLoadOperation.progress / 10f);
            yield return null;
        }
        loadingPanel.FinishProgress();
        _gameManager.StartGame();
    }

    void OnDestroy() {
        GameAnalytics?.Destroy();
        AdjustAnalytics?.Destroy();
    }

    void OnValidate() {
        if (!analyticsConfig.useAnalytics) {
            analyticsConfig.useAdjust = false;
            analyticsConfig.useGameAnalytics = false;
        }

        AdNetwork[] networks = new AdNetwork[adConfig.adServices.Length];
        for(int i = 0; i < networks.Length; i++) networks[i] = adConfig.adServices[i].network;
        if (ListUtils.HasDuplicates<AdNetwork>(networks)) {
            Debug.LogError("Duplicate ad networks found");
        }
    }
}
