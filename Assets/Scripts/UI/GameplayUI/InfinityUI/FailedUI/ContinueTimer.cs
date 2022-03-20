﻿using System;
using System.Collections;
using Core;
using Progress;
using UI.InfinityUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ContinueTimer : MonoBehaviour
    {
        [SerializeField] private Image timer;
        [SerializeField] private Text timerText;
        [SerializeField] private int timerValue;
        [SerializeField] private FailedInfinityUI failedInfinityUI;
        [SerializeField] private ScorePanel scorePanel;
        [SerializeField] private ProgressController progressController;
        private int currentBestScore;
        public void StartTimer()
        {
            currentBestScore = progressController.currentState.bestScore;
            timer.fillAmount = 1;
            timerValue = 5;
            timerText.text = timerValue.ToString();
            StartCoroutine(Timer());
            StartCoroutine(NumberTimer());
        }
        
        public void ShowNextUI()
        {
            if (scorePanel.GetPoints() > currentBestScore)
                failedInfinityUI.ShowNewBestScoreUI();
            else
                failedInfinityUI.ShowStatisticsUI();
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