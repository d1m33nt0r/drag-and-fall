using Data.Core;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FinishLevel
{
    public class FirstRewardSlot : MonoBehaviour
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
            if (progressController.levelsProgress.levelsProgresses[levelData.levelIndex].rewardIsIssued[0])
            {
                rewardSizeText.text = "Issued";
            }
            else
            {
                var randCoinsCount = Random.Range(100, 301);
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
            progressController.currentState.currenciesProgress.coins += count;
            progressController.levelsProgress.levelsProgresses[levelData.levelIndex].rewardIsIssued[0] = true;
            progressController.SaveCurrentState(progressController.currentState);
            progressController.SaveLevelsProgress(progressController.levelsProgress);
        }
    }
}