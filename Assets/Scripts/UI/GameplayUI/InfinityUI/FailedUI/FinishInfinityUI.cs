using UnityEngine;
using UnityEngine.UI;

namespace UI.GameplayUI.InfinityUI.FailedUI
{
    public class FinishInfinityUI : MonoBehaviour
    {
        [SerializeField] private Text crystalText;
        [SerializeField] private Text coinText;
        [SerializeField] private Text keyText;
        [SerializeField] private Text scoreText;
        
        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
        
        public void SetCurrencies(int coins, int crystals, int keys)
        {
            coinText.text = coins.ToString();
            crystalText.text = crystals.ToString();
            keyText.text = keys.ToString();
        }
    }
}