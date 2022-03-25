using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class InterstitialAds : MonoBehaviour
    {
        private InterstitialAd interstitial;

        public void RequestInterstitial()
        {
            if (interstitial != null)
            {
                interstitial.Destroy();
            }
            
            var adUnitId = "ca-app-pub-3940256099942544/1033173712";
            interstitial = new InterstitialAd(adUnitId);
            
            interstitial.OnAdLoaded += HandleOnAdLoaded;
            interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            interstitial.OnAdOpening += HandleOnAdOpened;
            interstitial.OnAdClosed += HandleOnAdClosed;

            var request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            
        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.LoadAdError);
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            Time.timeScale = 0;
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitial();
            MonoBehaviour.print("HandleAdClosed event received");
            Time.timeScale = 1;
        }
        
        public bool TryShowInterstitialAd()
        {
            if (interstitial != null && interstitial.IsLoaded())
            {
                interstitial.Show();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}