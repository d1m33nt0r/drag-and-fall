using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class InterstitialAds : MonoBehaviour
    {
        private const string adUnitID = "ca-app-pub-3940256099942544/1033173712";
        private InterstitialAd interstitial;
        private AdRequest request;

        private void Awake()
        {
            interstitial = new InterstitialAd(adUnitID);
            request = new AdRequest.Builder().Build();
            interstitial.OnAdLoaded += HandleOnAdLoaded;
            interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            interstitial.OnAdOpening += HandleOnAdOpened;
            interstitial.OnAdClosed += HandleOnAdClosed;
        }

        public void RequestInterstitial()
        {
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