using System;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class CoinPanel : MonoBehaviour
    {
        public Text coins;
        public ProgressController progressController;
        
        public void AddCoins(int count)
        {
            var convertedText = Convert.ToInt32(coins.text);
            convertedText += count;
            coins.text = convertedText.ToString();
        }

        public void Start()
        {
            coins.text = progressController.currentState.currenciesProgress.coins.ToString();
        }

        public void SaveProgress()
        {
            progressController.SaveCurrentState(new CurrentState{
                currenciesProgress = new CurrenciesProgress
                {
                    coins = Convert.ToInt32(coins.text),
                    crystals = progressController.currentState.currenciesProgress.crystals,
                    keys = progressController.currentState.currenciesProgress.keys
                },
                environmentSkin = progressController.currentState.environmentSkin,
                fallingTrailSkin = progressController.currentState.fallingTrailSkin,
                trailSkin = progressController.currentState.trailSkin,
                playerSkin = progressController.currentState.playerSkin
            });
        }
    }
}