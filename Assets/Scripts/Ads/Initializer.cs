using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private BannerAds bannerAds;
        
        private void Awake()
        {
            MobileAds.Initialize(initStatus => { });
        }

        private void Start()
        {
            bannerAds.RequestBanner();
        }
    }
}