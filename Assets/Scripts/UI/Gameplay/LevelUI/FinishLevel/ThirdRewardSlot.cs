using Core;
using Data.Core;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FinishLevel
{
    public class ThirdRewardSlot : MonoBehaviour
    {
        [SerializeField] private Text rewardText;
        [SerializeField] private Image rewardImage;
        [SerializeField] private Text rewardSizeText;
        [SerializeField] private ProgressController progressController;
        [SerializeField] private GameObject starsEffect;
        [SerializeField] private KeyPanel keyPanel;
        
        private LevelData levelData;
        
        public void SetLevelData(LevelData levelData)
        {
            this.levelData = levelData;
            starsEffect.SetActive(false);
        }
        
        public void ShowReward()
        {
            if (progressController.levelsProgress.levelsProgresses[levelData.levelIndex].rewardIsIssued[2])
            {
                rewardSizeText.text = "Issued";
            }
            else
            {
                var randCoinsCount = Random.Range(1, 4);
                rewardSizeText.text = randCoinsCount.ToString();
                IssueReward(randCoinsCount);
            }

            starsEffect.SetActive(true);
        }
        
        public void SetDefaultState()
        {
            rewardText.transform.localScale = Vector3.one;
            rewardImage.gameObject.SetActive(false);
            rewardSizeText.text = "?";
        }
        
        private void IssueReward(int count)
        {
            keyPanel.AddKeys(count);
            progressController.currentState.currenciesProgress.keys += count;
            progressController.levelsProgress.levelsProgresses[levelData.levelIndex].rewardIsIssued[2] = true;
            progressController.SaveCurrentState(progressController.currentState);
            progressController.SaveLevelsProgress(progressController.levelsProgress);
        }
    }
}