using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class BannerAds : MonoBehaviour
    {
        private BannerView bannerView;

        public void RequestBanner()
        {
            var adUnitId = "ca-app-pub-3940256099942544/6300978111";
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            var request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
            
            bannerView.OnAdLoaded += HandleOnAdLoaded;
            bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            bannerView.OnAdOpening += HandleOnAdOpened;
            bannerView.OnAdClosed += HandleOnAdClosed;
        }
        
        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.LoadAdError);
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
    }
}