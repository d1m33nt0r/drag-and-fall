using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core
{
    public class LevelButton : Button
    {
        private Tube tube;
        
        [SerializeField] private Text levelText;
        [SerializeField] private Text levelText2;
        [SerializeField] private Text levelText3;
        
        private int levelIndex;
        
        [SerializeField] private GameObject stars3Prefab;
        [SerializeField] private GameObject stars2Prefab;
        [SerializeField] private GameObject stars1Prefab;

        [SerializeField] private GameObject lockStatePrefab;
        [SerializeField] private GameObject unlockStatePrefab;
        [SerializeField] private GameObject completedStatePrefab;

        public override void OnPointerDown(PointerEventData pointerEventData)
        {
            tube.EnableLevelMode(transform.parent.parent.GetComponent<LevelUIController>().levelsData.leves[levelIndex]);
            tube.SetLevelMode(true);
            tube.gameManager.StartedLevel();
        }
        
        private void Awake()
        {
            tube = transform.parent.parent.GetComponent<LevelUIController>().tube;
        }

        public void SetLevelIndex(int levelIndex)
        {
            this.levelIndex = levelIndex;
        }
        
        public void SetUnlockState()
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<Button>().interactable = true;
            completedStatePrefab.SetActive(false);
            lockStatePrefab.SetActive(false);
            unlockStatePrefab.SetActive(true);
        }

        public void SetLevelText(string levelText)
        {
            this.levelText.text = levelText;
            levelText2.text = levelText;
            levelText3.text = levelText;
        }

        public void SetLockState()
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<Button>().interactable = false;
            completedStatePrefab.SetActive(false);
            lockStatePrefab.SetActive(true);
            unlockStatePrefab.SetActive(false);
        }

        public void SetCompletedState(int countStars)
        {
            gameObject.SetActive(true);
            completedStatePrefab.SetActive(true);
            lockStatePrefab.SetActive(false);
            unlockStatePrefab.SetActive(false);
            switch (countStars)
            {
                case 1:
                    stars1Prefab.SetActive(true);
                    stars2Prefab.SetActive(false);
                    stars3Prefab.SetActive(false);
                    break;
                case 2:
                    stars1Prefab.SetActive(false);
                    stars2Prefab.SetActive(true);
                    stars3Prefab.SetActive(false);
                    break;
                case 3:
                    stars1Prefab.SetActive(false);
                    stars2Prefab.SetActive(false);
                    stars3Prefab.SetActive(true);
                    break;
            }
        }
    }
}