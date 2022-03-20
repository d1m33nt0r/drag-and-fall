using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InfinityUI
{
    public class BestScoreUI : MonoBehaviour
    {
        [SerializeField] private FailedInfinityUI failedInfinityUI;
        [SerializeField] private Text bestScoreText;
        [SerializeField] private ProgressController progressController;
        
        public void SetBestScoreText(int score)
        {
            bestScoreText.text = score.ToString();
            progressController.currentState.bestScore = score;
            progressController.SaveCurrentState(progressController.currentState);
        }
        
        public void ShowStatisticsUI()
        {
            failedInfinityUI.ShowStatisticsUI();
        }
    }
}