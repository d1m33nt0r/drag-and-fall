using System.Collections;
using Core;
using Data;
using UI.GameplayUI.InfinityUI.FailedUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InfinityUI
{
    public class FailedInfinityUI : MonoBehaviour
    {
        [SerializeField] private GameObject newBestScoreUI;
        [SerializeField] private GameObject continueUI;
        [SerializeField] private GameObject finishUI;
        [SerializeField] private ScorePanel scorePanel;
        [SerializeField] private SessionData sessionData;
        [SerializeField] private Text bestScoreText;
        
        private Coroutine newBestScoreCoroutine;
        
        public void ShowNewBestScoreUI()
        {
            newBestScoreUI.SetActive(true);
            //newBestScoreUI.GetComponent<BestScoreUI>().SetBestScoreText(scorePanel.GetPoints());
            continueUI.SetActive(false);
            finishUI.SetActive(false);
            if (newBestScoreCoroutine != null) StopCoroutine(newBestScoreCoroutine);
            newBestScoreCoroutine = StartCoroutine(AnimateScore(scorePanel.GetPoints()));
        }

        private IEnumerator AnimateScore(int maximum)
        {
            var t = 0f;
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                var sad = (int) Mathf.Lerp(0, maximum, t);
                bestScoreText.text = sad.ToString();
                t += 0.5f * Time.deltaTime;
                if (t > 1.0f) break;
                yield return null;
            }
        }
        
        public void ShowStatisticsUI()
        {
            newBestScoreUI.SetActive(false);
            continueUI.SetActive(false);
            finishUI.SetActive(true);
            finishUI.GetComponent<FinishInfinityUI>().SetCurrencies(sessionData.coins, sessionData.crystals, sessionData.keys);
            finishUI.GetComponent<FinishInfinityUI>().SetScore(scorePanel.GetPoints());
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