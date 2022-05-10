using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameplayUI.InfinityUI.FailedUI
{
    public class FinishInfinityUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI crystalText;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI keyText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public float minimum = -1.0F;
        public float maximum =  1.0F;
        
        private float t = 0.0f;
        
        private Coroutine coroutine;
        
        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
        
        public void SetCurrencies(int coins, int crystals, int keys)
        {
            coinText.text = coins.ToString();
            crystalText.text = crystals.ToString();
            keyText.text = keys.ToString();
            //if (coroutine != null) StopCoroutine(coroutine);
            //coroutine = StartCoroutine(AnimateCurrencies(coins, crystals, keys));
        }

        private IEnumerator AnimateCurrencies(int maximumCoins, int maximumCrystal, int maximumKeys)
        {
            while (true)
            {
                var tempValue = (int) Mathf.Lerp(0, maximumCoins, t);
                coinText.text = tempValue.ToString();
                
                t += 0.5f * Time.deltaTime;
                
                if (t > 1.0f)
                {
                    t = 0.0f;
                    break;
                }
                
                yield return null;
            }

            while (true)
            {
                var tempValue = (int) Mathf.Lerp(0, maximumCrystal, t);
                crystalText.text = tempValue.ToString();
                
                t += 0.5f * Time.deltaTime;
                
                if (t > 1.0f)
                {
                    t = 0.0f;
                    break;
                }
            }
            
            while (true)
            {
                var tempValue = (int) Mathf.Lerp(0, maximumKeys, t);
                keyText.text = tempValue.ToString();
                
                t += 0.5f * Time.deltaTime;
                
                if (t > 1.0f)
                {
                    t = 0.0f;
                    break;
                }
            }
        }
    }
}