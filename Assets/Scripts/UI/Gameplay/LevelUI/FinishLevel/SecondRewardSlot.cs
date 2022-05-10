using Core;
using Data.Core;
using Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FinishLevel
{
    public class SecondRewardSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI rewardSizeText;
        [SerializeField] private ProgressController progressController;
        [SerializeField] private GameObject starsEffect;
        [SerializeField] private CrystalPanel crystalPanel;
        
        private LevelData levelData;
        
        public void SetLevelData(LevelData levelData)
        {
            this.levelData = levelData;
            starsEffect.SetActive(false);
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
            crystalPanel.AddCrystals(count);
            progressController.currentState.currenciesProgress.crystals += count;
            progressController.levelsProgress.levelsProgresses[levelData.levelIndex].rewardIsIssued[1] = true;
            progressController.SaveCurrentState(progressController.currentState);
            progressController.SaveLevelsProgress(progressController.levelsProgress);
        }
    }
}