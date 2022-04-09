using System;
using Ads;
using Core.Bonuses;
using Data;
using Data.Core;
using GooglePlayGames;
using Progress;
using UI;
using UI.InfinityUI;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private RewardAds rewardAds;
        [SerializeField] private InterstitialAds interstitialAds;
        [SerializeField] private LevelProgress levelProgress;
        [SerializeField] private ScorePanel scorePanel;
        [SerializeField] private FinishLevelUI finishLevelUI;
        [SerializeField] private Player player;
        [SerializeField] public Animator cameraAnimator;
        [SerializeField] public PlatformMover platformMover;
        [SerializeField] public UIManager uiManager;
        [SerializeField] public GameMode gameMode;
        [SerializeField] public ShopController shopController;
        [SerializeField] private ProgressController progressController;
        [SerializeField] private DragController dragController;
        [SerializeField] private BonusController bonusController;
        [SerializeField] private CoinPanel coinPanel;
        [SerializeField] private CrystalPanel crystalPanel;
        [SerializeField] private KeyPanel keyPanel;
        [SerializeField] private FailedInfinityUI failedInfinityUI;
        [SerializeField] private SessionData sessionData;
        [SerializeField] private Concentration concentration;
        
        public bool gameStarted { get; private set; }
        
        private void Start()
        {
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate(success => { });
            platformMover.SetDefaultState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void StartGame()
        {
            concentration.Reset();
            bonusController.DeactivateAllBonuses();
            uiManager.SetActiveScorePanel(true);
            uiManager.scorePanel.ResetCounter();
            sessionData.ResetData();
            gameStarted = true;
            platformMover.SetDefaultState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(true);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void OpenShop()
        {
            cameraAnimator.Play("OpenShop");
            platformMover.SetShopState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveShopMenu(true);
            uiManager.SetDefaultStateForShop();
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }
        
        public void OpenLeaderboard()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }

        public void CloseShop()
        {
            cameraAnimator.Play("CloseShop");
            platformMover.SetDefaultState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void ChangeShopState(ShopState _shopState)
        {
            switch (_shopState)
            {
                case ShopState.EnvironmentSkin:
                    platformMover.SetShopState();
                    break;
                case ShopState.PlayerSkin:
                    platformMover.SetShopState();
                    break;
                case ShopState.TrailSkin:
                    platformMover.SetShopState();
                    break;
                case ShopState.FallingTrailSkin:
                    platformMover.SetShopFallingTrail();
                    break;
            }
        }

        public void ShowLevels()
        {
            uiManager.SetActiveScorePanel(false);
            platformMover.SetLevelMode(false);
            platformMover.SetDefaultState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(true);
        }

        public void FinishLevel(LevelData levelsData)
        {
            uiManager.SetActiveFinishLevel(true);
            finishLevelUI.SetLevelData(levelsData);
            finishLevelUI.SetMaxValue();
            finishLevelUI.SetText(scorePanel.GetPoints(), levelsData.levelIndex + 1);
            finishLevelUI.PlayShowAnimation();

            bonusController.DeactivateAllBonuses();
            gameStarted = false;
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveLevelsMenu(false);
            
            progressController.levelsProgress.levelsProgresses[levelsData.levelIndex].isCompleted = true;
            if (levelsData.levelIndex + 1 < progressController.levelsProgress.levelsProgresses.Count)
                progressController.levelsProgress.levelsProgresses[levelsData.levelIndex + 1].isUnlocked = true;
            
            progressController.SaveLevelsProgress(progressController.levelsProgress);
            
            coinPanel.SaveProgress();
            crystalPanel.SaveProgress();
            
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveGameMenu(false);
            
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
        }

        public void ShowMainMenu()
        {
            bonusController.DeactivateAllBonuses();
            concentration.Reset();
            uiManager.SetActiveScorePanel(false);
            //bonusController.DeactivateAllBonuses();
            platformMover.SetLevelMode(false);
            gameStarted = false;
            uiManager.SetActiveUpgradeMenu(false);
            platformMover.SetDefaultState();
            platformMover.InitializePlatforms();
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void ContinueGameAds()
        {
            rewardAds.TryShowRewardedAd();
            if (platformMover.isLevelMode)
                uiManager.SetActiveLevelUI(true);
            else
                uiManager.SetActiveGameMenu(true);
            
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            
            platformMover.DestroyPlatform(true);
            player.ContinueGame();
            gameStarted = true;
        }
        
        public void ContinueGameKeys()
        {
            if (platformMover.isLevelMode)
                uiManager.SetActiveLevelUI(true);
            else
                uiManager.SetActiveGameMenu(true);
            
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            
            platformMover.DestroyPlatform(true);
            player.ContinueGame();
            gameStarted = true;
        }
        
        public void StartNextLevel()
        {
            platformMover.transform.rotation = Quaternion.Euler(0, 0, 0);
            concentration.Reset();
            uiManager.scorePanel.ResetCounter();
            bonusController.DeactivateAllBonuses();
            gameStarted = true;
            platformMover.SetDefaultState();
            platformMover.gameManager.gameMode.levelMode.SetLevelData(
                platformMover.levelsData.leves[platformMover.gameManager.gameMode.levelMode.level.levelIndex + 1]);
            levelProgress.Initialize(platformMover.gameManager.gameMode.levelMode.level);
            platformMover.InitializePlatforms();
            
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(true);
            uiManager.SetActiveGameMenu(true);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void FailedGame()
        {
            interstitialAds.TryShowInterstitialAd();
           
            gameStarted = false;
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(true);
            uiManager.failedInfinityPanel.GetComponent<FailedInfinityUI>().ShowContinueUI();
            coinPanel.SaveProgress();
            crystalPanel.SaveProgress();
            keyPanel.SaveProgress();
            
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
            
        }

        public void FailedLevel()
        {
            platformMover.transform.rotation = Quaternion.Euler(0, 0, 0);
            interstitialAds.TryShowInterstitialAd();
            gameStarted = false;
            platformMover.SetDefaultState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(true);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void StartedLevel()
        {
            platformMover.transform.rotation = Quaternion.Euler(0, 0, 0);
            bonusController.DeactivateAllBonuses();
            concentration.Reset();
            sessionData.ResetData();
            uiManager.SetActiveScorePanel(true);
            uiManager.scorePanel.GetComponent<ScorePanel>().ResetCounter();
            gameStarted = true;
            platformMover.SetDefaultState();
            
            uiManager.SetActiveUpgradeMenu(false);
            platformMover.gameManager.gameMode.levelMode.ResetPointer();
            platformMover.InitializePlatforms();

            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(true);
            uiManager.SetActiveGameMenu(true);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void ShowUpgradeMenu()
        {
            //platformMover.SetLevelMode(false);
            gameStarted = false;
            uiManager.SetActiveUpgradeMenu(true);
            //platformMover.SetDefaultState();
            //platformMover.InitializePlatforms();
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }
        
        public void PauseGame()
        {
        }
    }
}