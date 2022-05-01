using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class InterstitialAds : MonoBehaviour
    {
        private const string adUnitID = "ca-app-pub-3940256099942544/1033173712";
        private InterstitialAd interstitial;

        public void RequestInterstitial()
        {
            if (interstitial != null)
            {
                interstitial.Destroy();
            }
            
            interstitial = new InterstitialAd(adUnitID);
            
            interstitial.OnAdLoaded += HandleOnAdLoaded;
            interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            interstitial.OnAdOpening += HandleOnAdOpened;
            interstitial.OnAdClosed += HandleOnAdClosed;

            var request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            
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
            interstitial.Destroy();
            RequestInterstitial();
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