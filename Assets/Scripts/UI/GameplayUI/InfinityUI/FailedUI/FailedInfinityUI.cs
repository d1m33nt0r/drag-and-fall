using Core;
using Data;
using UI.GameplayUI.InfinityUI.FailedUI;
using UnityEngine;

namespace UI.InfinityUI
{
    public class FailedInfinityUI : MonoBehaviour
    {
        [SerializeField] private GameObject newBestScoreUI;
        [SerializeField] private GameObject continueUI;
        [SerializeField] private GameObject finishUI;
        [SerializeField] private ScorePanel scorePanel;
        [SerializeField] private SessionData sessionData;
       
        public void ShowNewBestScoreUI()
        {
            newBestScoreUI.SetActive(true);
            newBestScoreUI.GetComponent<BestScoreUI>().SetBestScoreText(scorePanel.GetPoints());
            continueUI.SetActive(false);
            finishUI.SetActive(false);
        }

        public void ShowStatisticsUI()
        {
            finishUI.GetComponent<FinishInfinityUI>().SetCurrencies(sessionData.coins, sessionData.crystals, sessionData.keys);
            finishUI.GetComponent<FinishInfinityUI>().SetScore(scorePanel.GetPoints());
            newBestScoreUI.SetActive(false);
            continueUI.SetActive(false);
            finishUI.SetActive(true);
        }

        public void ShowContinueUI()
        {
            newBestScoreUI.SetActive(false);
            continueUI.SetActive(true);
            continueUI.GetComponent<ContinueTimer>().StartTimer();
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