using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private Text counter;
        [SerializeField] private GameObject counterPanel;
        
        private void Start()
        {
            ResetCounter();
        }

        public void AddPoints(int points)
        {
            var converted = Convert.ToInt32(counter.text);
            converted += points;
            counter.text = converted.ToString();
        }

        public void SetActiveCounterPanel(bool _value)
        {
            counterPanel.SetActive(_value);
        }
        
        public void ResetCounter()
        {
            counter.text = "0";
        }
    }
}