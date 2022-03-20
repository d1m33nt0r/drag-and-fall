using System;
using Core;
using Core.Bonuses;
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
        
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            text = GetComponent<Text>();
        }

        public void SetText(int countPoints)
        {
            var upgradedPoint = countPoints + bonusController.multiplier;
            text.text = "+" + Convert.ToString(upgradedPoint);
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