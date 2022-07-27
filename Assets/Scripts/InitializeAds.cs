using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static InitializeAds Instance;

    [SerializeField] private string gameId;
    [SerializeField] private bool testMode = false;

    [SerializeField] private string videoPlacementId = "video";
    [SerializeField] private string rewardedVideoPlacementId = "rewardedVideo";
    [SerializeField] private string bannerPlacementId = "banner";

    private void Awake()
    {
        Instance = this;
        Advertisement.Initialize(gameId, testMode, this);
        Advertisement.Load(videoPlacementId, this);
        Advertisement.Load(rewardedVideoPlacementId, this);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(bannerPlacementId);
    }

    void Start()
    {           
        Advertisement.Banner.Show(bannerPlacementId);
    }

    public void ShowInterstitialAd()
    {
        Debug.Log("Showing Ad: " + videoPlacementId);
        Advertisement.Show(videoPlacementId, this);
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(rewardedVideoPlacementId, this);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (rewardedVideoPlacementId.Equals(placementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            Crafter.Instance.Hint();
            // Load another ad:
            Advertisement.Load(rewardedVideoPlacementId, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {placementId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }
}
    