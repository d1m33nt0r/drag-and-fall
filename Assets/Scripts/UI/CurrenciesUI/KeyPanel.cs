using System;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class KeyPanel : MonoBehaviour
    {
        public Text keys;
        public ProgressController progressController;
        
        public void AddCoins(int count)
        {
            var convertedText = Convert.ToInt32(keys.text);
            convertedText += count;
            keys.text = convertedText.ToString();
        }

        public void Start()
        {
            keys.text = progressController.currentState.currenciesProgress.keys.ToString();
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
                playerSkin = progressController.currentState.playerSkin
            });
        }
    }
}