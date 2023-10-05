using SpecialItems;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string iosGameId;
    [SerializeField] private string iosRewardedId;
    [SerializeField] private string androidGameId;
    [SerializeField] private string androidRewardedId;

    [SerializeField] private Button adButton;

    private string _gameId;
    private string _rewardedId;

    private void Awake()
    {
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

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (_rewardedId.Equals(placementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            int randomItemId = Random.Range(0, 3);
            SpecialItemManager.GiveItem((SpecialItem) randomItemId);
        }
        
        LoadRewardedAd();
    }
}
