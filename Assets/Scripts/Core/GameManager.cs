using Data.Core;
using Progress;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] public Animator cameraAnimator;
        [SerializeField] public Tube tube;
        [SerializeField] public UIManager uiManager;
        [SerializeField] public GameMode gameMode;
        [SerializeField] public ShopController shopController;
        [SerializeField] private ProgressController progressController;
        [SerializeField] private DragController dragController;
        
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
            gameStarted = true;
            tube.platforms[0].DestroyPlatform();
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
        
        public void StartNextLevel()
        {
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
            tube.SetDefaultState();
            uiManager.SetActiveUpgradeMenu(false);
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveLevelUI(false);
            uiManager.SetActiveGameMenu(false);
            uiManager.SetActiveFinishLevel(false);
            uiManager.SetActiveFailedInfinityPanel(true);
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