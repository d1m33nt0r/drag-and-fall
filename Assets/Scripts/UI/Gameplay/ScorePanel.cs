using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private Text counter;
        [SerializeField] private GameObject counterPanel;
        [SerializeField] private Text score;
        
        private void Start()
        {
            ResetCounter();
        }

        public int GetPoints()
        {
            return Convert.ToInt32(counter.text);
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
            score.gameObject.SetActive(_value);
            counter.gameObject.SetActive(_value);
        }
        
        public void ResetCounter()
        {
            counter.text = "0";
        }
    }
}