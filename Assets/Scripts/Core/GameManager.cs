using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] public UIManager uiManager;
        [SerializeField] public GameMode gameMode;
        
        public bool gameStarted { get; private set; }

        private void Start()
        {
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveGameMenu(false);
        }

        public void StartGame()
        {
            gameStarted = true;
            gameMode.ActiveInfinityMode();
            uiManager.SetActiveMainMenu(false);
            uiManager.SetActiveGameMenu(true);
            
        }

        public void FinishGame()
        {
            gameStarted = false;
            uiManager.SetActiveMainMenu(true);
            uiManager.SetActiveGameMenu(false);
        }

        public void PauseGame()
        {
            
        }
    }
}