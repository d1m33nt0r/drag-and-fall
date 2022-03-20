using Core.Bonuses;
using Data;
using Data.Core;
using Progress;
using UI.InfinityUI;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] public Animator cameraAnimator;
        [SerializeField] public Tube tube;
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
        
        public bool gameStarted { get; private set; }

        private void Start()
        {
            tube.SetDefaultState();
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
            uiManager.SetActiveScorePanel(true);
            uiManager.scorePanel.ResetCounter();
            sessionData.ResetData();
            gameStarted = true;
            tube.SetDefaultState();
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
            tube.SetShopState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveShopMenu(true);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void CloseShop()
        {
            cameraAnimator.Play("CloseShop");
            tube.SetDefaultState();
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
                    tube.SetShopState();
                    break;
                case ShopState.PlayerSkin:
                    tube.SetShopState();
                    break;
                case ShopState.TrailSkin:
                    tube.SetShopState();
                    break;
                case ShopState.FallingTrailSkin:
                    tube.SetShopFallingTrail();
                    break;
            }
        }

        public void ShowLevels()
        {
            uiManager.SetActiveScorePanel(false);
            tube.SetLevelMode(false);
            tube.SetDefaultState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(true);
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
            gameStarted = false;
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveLevelsMenu(false);
            progressController.levelsProgress.levelsProgresses[levelsData.levelIndex].isCompleted = true;
            if (levelsData.levelIndex + 1 < progressController.levelsProgress.levelsProgresses.Count)
                progressController.levelsProgress.levelsProgresses[levelsData.levelIndex + 1].isUnlocked = true;
            progressController.SaveLevelsProgress(progressController.levelsProgress);
            coinPanel.SaveProgress();
            crystalPanel.SaveProgress();
            uiManager.UpdateLevelsStatus();
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(true);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
        }

        public void ShowMainMenu()
        {
            uiManager.SetActiveScorePanel(false);
            bonusController.DeactivateAllBonuses();
            tube.SetLevelMode(false);
            gameStarted = false;
            uiManager.SetActiveUpgradeMenu(false);
            tube.SetDefaultState();
            tube.InitializePlatforms();
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void ContinueGame()
        {
            tube.platforms[0].DestroyPlatform();
            player.ContinueGame();
            gameStarted = true;
            uiManager.SetActiveGameMenu(true);
            uiManager.SetActiveFailedInfinityPanel(false);
        }
        
        public void StartNextLevel()
        {
            uiManager.scorePanel.ResetCounter();
            bonusController.DeactivateAllBonuses();
            gameStarted = true;
            tube.SetDefaultState();
            tube.gameManager.gameMode.levelMode.SetLevelData(
                tube.levelsData.leves[tube.gameManager.gameMode.levelMode.level.levelIndex + 1]);
            tube.InitializePlatforms();
            
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(true);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void FailedGame()
        {
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
            gameStarted = false;
            tube.SetDefaultState();
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
            sessionData.ResetData();
            uiManager.SetActiveScorePanel(true);
            uiManager.scorePanel.GetComponent<ScorePanel>().ResetCounter();
            gameStarted = true;
            tube.SetDefaultState();

            uiManager.SetActiveUpgradeMenu(false);
            tube.gameManager.gameMode.levelMode.ResetPointer();
            tube.InitializePlatforms();

            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(true);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(false);
            uiManager.SetActiveFailedLevelPanel(false);
            uiManager.SetActiveLevelsMenu(false);
        }

        public void ShowUpgradeMenu()
        {
            tube.SetLevelMode(false);
            gameStarted = false;
            uiManager.SetActiveUpgradeMenu(true);
            tube.SetDefaultState();
            tube.InitializePlatforms();
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