using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private RewardAds rewardAds;
        [SerializeField] private InterstitialAds interstitialAds;
        
        private void Awake()
        {
            MobileAds.Initialize(initStatus => { });
        }

        private void Start()
        {
            rewardAds.RequestReward();
            interstitialAds.RequestInterstitial();
        }
    }
}