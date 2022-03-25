using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class RewardAds : MonoBehaviour
    {
        private RewardedAd rewardedAd;
        
        public void RequestReward()
        {
            var adUnitId = "ca-app-pub-3940256099942544/5224354917";

            rewardedAd = new RewardedAd(adUnitId);
            
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;
            
            var request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
        }
        
        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            var request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
            MonoBehaviour.print("HandleAdClosed event received");
            Time.timeScale = 1;
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            Time.timeScale = 0;
            MonoBehaviour.print("HandleRewardedAdOpening event received");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            Time.timeScale = 1;
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: "
                + args.Message);
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            var request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
            MonoBehaviour.print("HandleAdClosed event received");
            Time.timeScale = 1;
            MonoBehaviour.print("HandleRewardedAdClosed event received");
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            var request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
            MonoBehaviour.print("HandleAdClosed event received");
            Time.timeScale = 1;
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print(
                "HandleRewardedAdRewarded event received for "
                + amount.ToString() + " " + type);
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