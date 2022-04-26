using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class RewardAds : MonoBehaviour
    {
        private const string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        private RewardedAd rewardedAd;
        private AdRequest adRequest;

        private void Awake()
        {
            adRequest = new AdRequest.Builder().Build();
        }

        public void RequestReward()
        {
            rewardedAd = new RewardedAd(adUnitId);
            
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;
            
            rewardedAd.LoadAd(adRequest);
        }
        
        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
           
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            rewardedAd.LoadAd(adRequest);
            Time.timeScale = 1;
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            Time.timeScale = 0;
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            Time.timeScale = 1;
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            var request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
            Time.timeScale = 1;
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            var request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
            Time.timeScale = 1;
        }
        
        public void TryShowRewardedAd()
        {
            if (rewardedAd != null && rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
            }
        }
    }
}