using System;
using Core;
using GoogleMobileAds.Api;
using UI.Gameplay;
using UnityEngine;

namespace Ads
{
    public class RewardAds : MonoBehaviour
    {
        private RewardedAd rewardedAd;
        private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5224354917";
        private AdRequest request;
        [SerializeField] private GameplayUI gameplayUI;
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private Player player;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameManager gameManager;
        
        private void Awake()
        {
            rewardedAd = new RewardedAd(AD_UNIT_ID);
            request = new AdRequest.Builder().Build();
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        }

        public void RequestReward()
        {
            rewardedAd.LoadAd(request);
        }
        
        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            rewardedAd.LoadAd(request);
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {

        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
   
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            rewardedAd.LoadAd(request);
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            if (platformMover.isLevelMode)
            {
                gameplayUI.EnableLevelMode();
            }
            else
            {
                gameplayUI.EnableInfinityMode();
            }
            
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            
            platformMover.DestroyPlatform(true);
            player.ContinueGame();
            gameManager.gameStarted = true;
            
            rewardedAd.LoadAd(request);
            var type = args.Type;
            var amount = args.Amount;
        }
        
        public void TryShowRewardedAd()
        {
            if (rewardedAd != null && rewardedAd.IsLoaded())
                rewardedAd.Show();
        }
    }
}