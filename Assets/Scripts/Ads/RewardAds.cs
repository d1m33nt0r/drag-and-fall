using System;
using Core;
using Data;
using GoogleMobileAds.Api;
using TMPro;
using UI.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Ads
{
    public class RewardAds : MonoBehaviour
    {
        private RewardedAd rewardedAd;
        private RewardedAd doublingAd;
        private const string AD_UNIT_ID = "ca-app-pub-8482915708082945/2776152632";//"ca-app-pub-3940256099942544/5224354917";
        private const string DOUBLING_AD_UNIT_ID = "ca-app-pub-8482915708082945/6621341957";
        private AdRequest request;
        [SerializeField] private GameplayUI gameplayUI;
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private Player player;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private SessionData sessionData;
        [SerializeField] private CoinPanel coinPanel;
        [SerializeField] private CrystalPanel crystalPanel;
        [SerializeField] private KeyPanel keyPanel;

        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI crystaText;
        [SerializeField] private TextMeshProUGUI keyText;
        
        private void Awake()
        {
            rewardedAd = new RewardedAd(AD_UNIT_ID);
            doublingAd = new RewardedAd(DOUBLING_AD_UNIT_ID);
            request = new AdRequest.Builder().Build();
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            doublingAd.OnUserEarnedReward += HandleUserEarnedDoublingReward;
            doublingAd.OnAdClosed += HandleDoublingRewardedAdClosed;
        }

        public void RequestReward()
        {
            rewardedAd.LoadAd(request);
            doublingAd.LoadAd(request);
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
        
        public void TryShowDoublingRewardedAd()
        {
            if (doublingAd != null && doublingAd.IsLoaded())
                doublingAd.Show();
        }
        
        public void HandleUserEarnedDoublingReward(object sender, Reward args)
        {
            coinPanel.AddCoins(sessionData.coins);
            crystalPanel.AddCrystals(sessionData.crystals);
            keyPanel.AddKeys(sessionData.keys);
            
            gameManager.ShowMainMenu();
            
            doublingAd.LoadAd(request);
            var type = args.Type;
            var amount = args.Amount;
        }
        
        public void HandleDoublingRewardedAdClosed(object sender, EventArgs args)
        {
            doublingAd.LoadAd(request);
            gameManager.ShowMainMenu();
        }
    }
}