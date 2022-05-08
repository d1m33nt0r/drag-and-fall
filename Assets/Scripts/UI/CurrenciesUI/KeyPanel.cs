using System;
using Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class KeyPanel : MonoBehaviour
    {
        public TextMeshProUGUI keys;
        public ProgressController progressController;

        public void AddKeys(int count)
        {
            var convertedText = Convert.ToInt32(keys.text);
            convertedText += count;
            keys.text = convertedText.ToString();
        }

        public void Start()
        {
            keys.text = progressController.currentState.currenciesProgress.keys.ToString();
        }

        public void MinusKeys(int keys)
        {
            this.keys.text = (Convert.ToInt32(this.keys.text) - keys).ToString();
            SaveProgress();
        }
        
        public void SaveProgress()
        {
            progressController.SaveCurrentState(new CurrentState{
                currenciesProgress = new CurrenciesProgress
                {
                    coins = progressController.currentState.currenciesProgress.coins,
                    crystals = progressController.currentState.currenciesProgress.crystals,
                    keys = Convert.ToInt32(keys.text)
                },
                environmentSkin = progressController.currentState.environmentSkin,
                fallingTrailSkin = progressController.currentState.fallingTrailSkin,
                trailSkin = progressController.currentState.trailSkin,
                playerSkin = progressController.currentState.playerSkin,
                bestScore = progressController.currentState.bestScore
            });
        }
    }
}