using SpecialItems;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdManager Instance { get; private set; }
    
    [SerializeField] private string iosGameId;
    [SerializeField] private string iosRewardedId;
    [SerializeField] private string androidGameId;
    [SerializeField] private string androidRewardedId;

    [SerializeField] private Button adButton;

    private string _gameId;
    private string _rewardedId;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            adButton.onClick.AddListener(Instance.PlayRewardedAd);
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
#if UNITY_IOS
        _gameId = iosGameId;
        _rewardedId = iosRewardedId;
#else
        GameId = AndroidGameId;
        RewardedId = AndroidRewardedId;
#endif
        
        adButton.interactable = false;
        Advertisement.Initialize(_gameId, false, this);
    }

    public void LoadRewardedAd()
    {
        if (!Advertisement.isInitialized)
            return;
        
        Advertisement.Load(_rewardedId, this);
    }

    public void PlayRewardedAd()
    {
        adButton = GameObject.Find("Ad Button").GetComponent<Button>();
        adButton.interactable = false;
        Advertisement.Show(_rewardedId, this);
    }

    public void OnInitializationComplete()
    {
        LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Advertisement.Initialize(_gameId, false, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        adButton.interactable = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        LoadRewardedAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId)
    {
        LoadRewardedAd();
    }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (!_rewardedId.Equals(placementId) || !showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) 
            return;
        
        GameObject.Find("MainMenuManager").GetComponent<MainMenu>().ActivateRewardPanel();
    }
}
