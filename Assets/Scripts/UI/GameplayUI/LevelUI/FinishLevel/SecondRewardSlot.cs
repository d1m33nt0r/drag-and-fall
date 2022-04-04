using Data.Core;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FinishLevel
{
    public class SecondRewardSlot : MonoBehaviour
    {
        [SerializeField] private Text rewardText;
        [SerializeField] private Image rewardImage;
        [SerializeField] private Text rewardSizeText;
        [SerializeField] private ProgressController progressController;
        
        private LevelData levelData;
        
        public void SetLevelData(LevelData levelData)
        {
            this.levelData = levelData;
        }
        
        public void ShowReward()
        {
            if (progressController.levelsProgress.levelsProgresses[levelData.levelIndex].rewardIsIssued[1])
            {
                rewardSizeText.text = "Issued";
            }
            else
            {
                var randCoinsCount = Random.Range(10, 31);
                rewardSizeText.text = randCoinsCount.ToString();
                IssueReward(randCoinsCount);
            }
        }
        
        public void SetDefaultState()
        {
            rewardText.transform.localScale = Vector3.one;
            rewardImage.gameObject.SetActive(false);
            rewardSizeText.text = "?";
        }
        
        private void IssueReward(int count)
        {
            progressController.currentState.currenciesProgress.crystals += count;
            progressController.levelsProgress.levelsProgresses[levelData.levelIndex].rewardIsIssued[1] = true;
            progressController.SaveCurrentState(progressController.currentState);
            progressController.SaveLevelsProgress(progressController.levelsProgress);
        }
    }
}