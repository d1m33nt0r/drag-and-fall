using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class InterstitialAds : MonoBehaviour
    {
        private const string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        private InterstitialAd interstitial;
        private AdRequest adRequest;

        private void Awake()
        {
            adRequest = new AdRequest.Builder().Build();
        }

        public void RequestInterstitial()
        {
            interstitial = new InterstitialAd(adUnitId);
            
            interstitial.OnAdLoaded += HandleOnAdLoaded;
            interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            interstitial.OnAdOpening += HandleOnAdOpened;
            interstitial.OnAdClosed += HandleOnAdClosed;
            
            interstitial.LoadAd(adRequest);
            
        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
          
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
           
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            Time.timeScale = 0;
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitial();
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