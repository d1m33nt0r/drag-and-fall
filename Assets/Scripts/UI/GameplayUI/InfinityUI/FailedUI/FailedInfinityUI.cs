using UnityEngine;

namespace UI.InfinityUI
{
    public class FailedInfinityUI : MonoBehaviour
    {
        [SerializeField] private GameObject newBestScoreUI;
        [SerializeField] private GameObject continueUI;
        [SerializeField] private GameObject finishUI;

        public void ShowNewBestScoreUI()
        {
            newBestScoreUI.SetActive(true);
            continueUI.SetActive(false);
            finishUI.SetActive(false);
        }

        public void ShowStatisticsUI()
        {
            newBestScoreUI.SetActive(false);
            continueUI.SetActive(false);
            finishUI.SetActive(true);
        }

        public void ShowContinueUI()
        {
            newBestScoreUI.SetActive(false);
            continueUI.SetActive(true);
            finishUI.SetActive(false);
        }


        public void HideAllUI()
        {
            newBestScoreUI.SetActive(false);
            continueUI.SetActive(false);
            finishUI.SetActive(false);    
        }
    }
}