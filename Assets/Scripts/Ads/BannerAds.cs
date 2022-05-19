using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class BannerAds : MonoBehaviour
    {
        private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/6300978111";
        
        
        private BannerView bannerView;
        private AdRequest request;
        
        private void Awake()
        {
            bannerView = new BannerView(AD_UNIT_ID, AdSize.Banner, AdPosition.Bottom);
            request = new AdRequest.Builder().Build();
            bannerView.OnAdLoaded += HandleOnAdLoaded;
            bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            bannerView.OnAdOpening += HandleOnAdOpened;
            bannerView.OnAdClosed += HandleOnAdClosed;
            RequestBanner();
        }

        public void RequestBanner()
        {
            bannerView.LoadAd(request);
        }
        
        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
           
        }
    }
}