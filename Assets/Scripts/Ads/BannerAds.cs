using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class BannerAds : MonoBehaviour
    {
        private const string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        private BannerView bannerView;
        private AdRequest adRequest;

        private void Awake()
        {
            adRequest = new AdRequest.Builder().Build();
        }

        public void RequestBanner()
        {
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            
            bannerView.LoadAd(adRequest);
            
            bannerView.OnAdLoaded += HandleOnAdLoaded;
            bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            bannerView.OnAdOpening += HandleOnAdOpened;
            bannerView.OnAdClosed += HandleOnAdClosed;
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