using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.UI;

public class UnityAdsMonetization : MonoBehaviour, IUnityAdsInitializationListener
{

    // Initialaize

    [SerializeField] string _androidGameId = "4391775";
    [SerializeField] string _iOSGameId = "4391774";
    [SerializeField] bool _testMode = true;
    public ObscuredInt RewardCoin;
    private string _gameId;

    private RewardAdsMontize rewardAdsMontize;
    private InitialazeAdsMonitize initialazeAdsMonitize;

    void Awake()
    {
        InitializeAds();
        rewardAdsMontize = GetComponent<RewardAdsMontize>();
        initialazeAdsMonitize = GetComponent<InitialazeAdsMonitize>();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        rewardAdsMontize.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        GameObject.Find("Canvas (1)").transform.Find("SomthingWrong...").gameObject.SetActive(true);
    }

    public bool GetCurrentAdsStatus()
    {
        return Advertisement.isInitialized;
    }



    //public bool OnlineMode;
    //public bool giftAds;
    //public MultiplayerSettingCanvas MSC;


    //private static bool isInitialized = false;

    //private bool Onlineproblem;

    //private void Start()
    //{
    //    Advertisement.AddListener(this);
    //    Advertisement.Initialize(GooglePlay_ID, TestMode);
    //    if (!isInitialized)
    //    {
    //        isInitialized = true;
    //        Advertisement.AddListener(this);
    //        Advertisement.Initialize(GooglePlay_ID, TestMode);
    //    }
    //}

    //public void DisplayInterstitialADOnlineGame()
    //{
    //    Advertisement.Show(myPlacementId2);
    //    Onlineproblem = true;
    //}

    //public void DispalyVedioAD()
    //{
    //    isInitialized = true;
    //    Advertisement.Show(myPlacementId);
    //}


    //public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    //{
    //    if (showResult == ShowResult.Finished && placementId == "Rewarded_Android")
    //    {
    //        if (giftAds)
    //        {
    //            LM.RewardGift(RewardCoin);
    //        }
    //    }
    //    else if (showResult == ShowResult.Finished && placementId == "Interstitial_Android")
    //    {
    //        if (OnlineMode)
    //        {
    //            MSC.AdsWatched();
    //            MSC.AdsCancel();
    //            Onlineproblem = true;
    //        }
    //    }
    //    else if (showResult == ShowResult.Skipped && placementId == "Interstitial_Android")
    //    {
    //        if (OnlineMode)
    //        {
    //            MSC.AdsWatched();
    //            Onlineproblem = true;
    //        }
    //    }
    //    else if (showResult == ShowResult.Failed && placementId == "Interstitial_Android")
    //    {
    //        if (OnlineMode)
    //        {
    //            MSC.AdsWatched();
    //            Onlineproblem = true;
    //        }
    //    }
    //    else if (showResult == ShowResult.Failed && placementId == "Rewarded_Android")
    //    {
    //        GameObject.Find("Canvas (1)").transform.Find("SomthingWrong...").gameObject.SetActive(true);


    //    }

    //    return;
    //}


    //public void OnUnityAdsReady(string placementId)
    //{
    //    if (placementId == myPlacementId)
    //    {

    //    }
    //}


    //public void OnUnityAdsDidError(string massage)
    //{
    //    Debug.Log(massage);
    //    if (OnlineMode && Onlineproblem)
    //    {
    //        MSC.AdsWatched();
    //    }
    //    if (giftAds && isInitialized)
    //    {
    //        GameObject.Find("Canvas (1)").transform.Find("SomthingWrong...").gameObject.SetActive(true);
    //        isInitialized = false;
    //    }
    //}

    //public void OnUnityAdsDidStart(string placementId)
    //{

    //}

    //public void OnDestroy()
    //{
    //     Advertisement.RemoveListener (this);
    //}
}
