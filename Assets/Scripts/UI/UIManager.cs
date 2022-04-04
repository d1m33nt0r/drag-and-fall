using System;
using UnityEngine;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas mainMenu;
        [SerializeField] private Canvas game;
        [SerializeField] private Canvas shop;
        [SerializeField] private Canvas levelUI;
        [SerializeField] private GameObject failedLevelPanel;
        public GameObject failedInfinityPanel;
        [SerializeField] private GameObject finishLevelPanel;
        [SerializeField] private GameObject levelsMenu;
        [SerializeField] private LevelUIController levelUIController;
        [SerializeField] private GameObject upgradeMenu;
        [SerializeField] private GameObject commonPanel;
        
        public ScorePanel scorePanel;

        private void Start()
        {
            SetActiveScorePanel(false);
        }

        public void SetActiveCommonPanel(bool value)
        {
            commonPanel.SetActive(value);
        }
        
        public void SetActiveUpgradeMenu(bool value) => upgradeMenu.SetActive(value);
        public void SetActiveScorePanel(bool value) => scorePanel.SetActiveCounterPanel(value);
        public void UpdateLevelsStatus() => levelUIController.InitializeLevels();
        public void SetActiveMainMenu(bool _value) => mainMenu.enabled = _value;
        public void SetActiveGameMenu(bool _value) => game.enabled = _value;
        public void SetActiveShopMenu(bool _value) => shop.enabled = _value;

        public void SetDefaultStateForShop()
        {
            shop.GetComponent<ShopController>().SetDefaultState();
        }
        
        public void SetActiveLevelUI(bool value) => levelUI.enabled = value;
        public void SetActiveFinishLevel(bool value) => finishLevelPanel.SetActive(value);
        public void SetActiveFailedInfinityPanel(bool value) => failedInfinityPanel.SetActive(value);
        public void SetActiveFailedLevelPanel(bool value) => failedLevelPanel.SetActive(value);
        public void SetActiveLevelsMenu(bool value)
        {
            levelsMenu.SetActive(value);
            SetActiveCommonPanel(!value);
        }
    }
}