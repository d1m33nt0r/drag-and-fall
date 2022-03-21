using System;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class CrystalPanel : MonoBehaviour
    {
        public Text crystals;
        public ProgressController progressController;
        
        public void AddCrystals(int count)
        {
            var convertedText = Convert.ToInt32(crystals.text);
            convertedText += count;
            crystals.text = convertedText.ToString();
        }

        public void Start()
        {
            crystals.text = progressController.currentState.currenciesProgress.crystals.ToString();
        }
        
        public void MinusCrystals(int crystals)
        {
            this.crystals.text = (Convert.ToInt32(this.crystals.text) - crystals).ToString();
            SaveProgress();
        }

        public void SaveProgress()
        {
            progressController.SaveCurrentState(new CurrentState{
                currenciesProgress = new CurrenciesProgress
                {
                    coins = progressController.currentState.currenciesProgress.coins,
                    crystals = Convert.ToInt32(crystals.text),
                    keys = progressController.currentState.currenciesProgress.keys
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