using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Core.MainMenuUI
{
    public class BestScoreMainText : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private ProgressController progressController;
        
        public void Start()
        {
            text.text = progressController.currentState.bestScore.ToString();
        }

        public void UpdateText()
        {
            text.text = progressController.currentState.bestScore.ToString();
        }
    }
}