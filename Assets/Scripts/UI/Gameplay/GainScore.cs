using System;
using System.Collections;
using Common;
using Core;
using Core.Bonuses;
using ObjectPool;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GainScore : MonoBehaviour
    {
        private Text text;
        private RectTransform rectTransform;
        public BonusController bonusController;
        public Concentration concentration;
        [SerializeField] private ScorePanel scorePanel;
        [SerializeField] private PlatformMover platformMover;
        
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            text = GetComponent<Text>();
        }

        public void SetText(int countPoints)
        {
            if (!platformMover.isLevelMode)
            {
                var concentrationMultiplier = concentration.isActive ? concentration.currentConcentrationMultiplier : 1;
                var bonusMultiplier = bonusController.multiplierIsActive ? bonusController.multiplier : 1;
                var upgradedPoint = (countPoints * bonusMultiplier) * concentrationMultiplier;
                text.text = Constants.GainScoreValues.GetString(upgradedPoint);
                scorePanel.AddPoints(upgradedPoint);
            }
            else
            {
                var concentrationMultiplier = concentration.isActive ? 2 : 1;
                var bonusMultiplier = bonusController.multiplierIsActive ? bonusController.multiplier : 1;
                var upgradedPoint = (countPoints * bonusMultiplier) * concentrationMultiplier;
                text.text = Constants.GainScoreValues.GetString(upgradedPoint);
                scorePanel.AddPoints(upgradedPoint);
            }
        }
        
        public void Animate()
        {
            rectTransform.localScale = new Vector3(2, 2, 2);
        }

        public void Update()
        {
            if(rectTransform.localScale != Vector3.zero)
                rectTransform.localScale -= new Vector3(.05f, .05f, .05f);
        }
    }
}