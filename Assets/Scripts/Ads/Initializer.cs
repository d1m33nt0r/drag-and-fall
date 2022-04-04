using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private BannerAds bannerAds;
        [SerializeField] private RewardAds rewardAds;
        [SerializeField] private InterstitialAds interstitialAds;
        
        private void Awake()
        {
            MobileAds.Initialize(initStatus => { });
        }

        private void Start()
        {
            bannerAds.RequestBanner();
            rewardAds.RequestReward();
            interstitialAds.RequestInterstitial();
        }
    }
}