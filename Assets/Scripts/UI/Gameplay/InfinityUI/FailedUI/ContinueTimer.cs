using System.Collections;
using Core;
using Progress;
using TMPro;
using UI.InfinityUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ContinueTimer : MonoBehaviour
    {
        [SerializeField] private Image timer;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private int timerValue;
        [SerializeField] private FailedInfinityUI failedInfinityUI;
        [SerializeField] private ScorePanel scorePanel;
        [SerializeField] private ProgressController progressController;
        [SerializeField] private KeySpender keySpender;
        [SerializeField] private TextMeshProUGUI buttonKeysText;

        private Coroutine timerCoroutine;
        private Coroutine numberCoroutine;
        
        private int currentBestScore;
        public void StartTimer()
        {
            buttonKeysText.text = keySpender.currentUsageStep.ToString();
            currentBestScore = progressController.currentState.bestScore;
            timer.fillAmount = 1;
            timerValue = 5;
            timerText.text = timerValue.ToString();
            if (timerCoroutine != null) StopCoroutine(timerCoroutine);
            if (numberCoroutine != null) StopCoroutine(numberCoroutine);
            timerCoroutine = StartCoroutine(Timer());
            numberCoroutine = StartCoroutine(NumberTimer());
        }
        
        public void ShowNextUI()
        {
            keySpender.ResetUsages();
            if (scorePanel.GetPoints() > currentBestScore) failedInfinityUI.ShowNewBestScoreUI();
            else failedInfinityUI.ShowStatisticsUI();
        }
        
        private IEnumerator Timer()
        {
            while (timer.fillAmount != 0)
            {
                timer.fillAmount -= Time.deltaTime / 5;
                yield return null;
            }
            
            ShowNextUI();
        }

        private IEnumerator NumberTimer()
        {
            while (timerValue != 0)
            {
                yield return new WaitForSeconds(1);
                timerValue--;
                timerText.text = timerValue.ToString();
            }
        }
    }
}