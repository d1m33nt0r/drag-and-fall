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
        
        public bool gameStarted { get; private set; }

        private void Start()
        {
            tube.SetDefaultState();
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(false);
        }

        public void StartGame()
        {
            gameStarted = true;
            tube.SetDefaultState();
            gameMode.ActiveInfinityMode();
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(true);
            
        }

        public void OpenShop()
        {
            cameraAnimator.Play("OpenShop");
            tube.SetShopState();
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveShopMenu(true);
            uiManager.SetActiveGameMenu(false);
        }
        
        public void CloseShop()
        {
            cameraAnimator.Play("CloseShop");
            tube.SetDefaultState();
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(false);
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
        
        public void FinishGame()
        {
            gameStarted = false;
            tube.SetDefaultState();
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveShopMenu(false);
            uiManager.SetActiveGameMenu(false);
        }

        public void PauseGame()
        {
            
        }
    }
}