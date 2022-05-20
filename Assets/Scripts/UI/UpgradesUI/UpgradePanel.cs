using Core;
using Core.Bonuses;
using Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Upgrades
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private BonusController bonusController;
        [Inject] private SoundOfBuying soundOfBuying;       
        [SerializeField] private ProgressController progressController;
        [SerializeField] private BonusType bonusType;
        [SerializeField] private Concentration concentration;
        [SerializeField] private TextMeshProUGUI firstLevelText;
        [SerializeField] private CoinPanel coinPanel;
        [SerializeField] private CrystalPanel crystalPanel;
        public Image[] levels;

        public void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            for (var i = 0; i < levels.Length; i++)
            {
                switch (bonusType)
                {
                    case BonusType.Acceleration:
                        levels[i].enabled = progressController.upgradeProgress.progressAcceleration[i];
                        break;
                    case BonusType.Magnet:
                        levels[i].enabled = progressController.upgradeProgress.progressMagnet[i];
                        break;
                    case BonusType.Multiplier:
                        levels[i].enabled = progressController.upgradeProgress.progressMultiplier[i];
                        break;
                    case BonusType.Shield:
                        levels[i].enabled = progressController.upgradeProgress.progressShield[i];
                        break;
                    case BonusType.None:
                        levels[i].enabled = progressController.upgradeProgress.progressConcentration[i];
                        if (!progressController.upgradeProgress.progressConcentration[i]) break;
                        if (concentration.currentConcentrationLevel % 2 != 0) firstLevelText.text = "It takes 1 less broken platform to replenish the concentration";
                        else firstLevelText.text = "+1 extra point when active concentration";
                        break;
                }
            }
        }

        public void UpgradeBonus()
        {
            if (progressController.currentState.currenciesProgress.coins < 500 
                || progressController.currentState.currenciesProgress.crystals < 15) return;
            
            soundOfBuying.PlayBuyingSound();
            coinPanel.MinusCoins(500);
            crystalPanel.MinusCrystals(15);
            
            switch (bonusType)
            {
                case BonusType.Acceleration:
                    for (var i = 0; i < progressController.upgradeProgress.progressAcceleration.Length; i++)
                    {
                        if (!progressController.upgradeProgress.progressAcceleration[i])
                        {
                            progressController.upgradeProgress.progressAcceleration[i] = true;
                            progressController.SaveUpgradeProgress(progressController.upgradeProgress);
                            Initialize();
                            bonusController.UpdateBonusLevels();
                            return;
                        }
                    }
                    break;
                case BonusType.Magnet:
                    for (var i = 0; i < progressController.upgradeProgress.progressMagnet.Length; i++)
                    {
                        if (!progressController.upgradeProgress.progressMagnet[i])
                        {
                            progressController.upgradeProgress.progressMagnet[i] = true;
                            progressController.SaveUpgradeProgress(progressController.upgradeProgress);
                            Initialize();
                            bonusController.UpdateBonusLevels();
                            return;
                        }
                    }
                    break;
                case BonusType.Multiplier:
                    for (var i = 0; i < progressController.upgradeProgress.progressMultiplier.Length; i++)
                    {
                        if (!progressController.upgradeProgress.progressMultiplier[i])
                        {
                            progressController.upgradeProgress.progressMultiplier[i] = true;
                            progressController.SaveUpgradeProgress(progressController.upgradeProgress);
                            Initialize();
                            bonusController.UpdateBonusLevels();
                            return;
                        }
                    }
                    break;
                case BonusType.Shield:
                    for (var i = 0; i < progressController.upgradeProgress.progressShield.Length; i++)
                    {
                        if (!progressController.upgradeProgress.progressShield[i])
                        {
                            progressController.upgradeProgress.progressShield[i] = true;
                            progressController.SaveUpgradeProgress(progressController.upgradeProgress);
                            Initialize();
                            bonusController.UpdateBonusLevels();
                            return;
                        }
                    }
                    break;
                case BonusType.None:
                    for (var i = 0; i < progressController.upgradeProgress.progressConcentration.Length; i++)
                    {
                        if (!progressController.upgradeProgress.progressConcentration[i])
                        {
                            progressController.upgradeProgress.progressConcentration[i] = true;
                            progressController.SaveUpgradeProgress(progressController.upgradeProgress);
                            Initialize();
                            concentration.UpdateLevel();
                            if (concentration.currentConcentrationLevel % 2 != 0) firstLevelText.text = "It takes 1 less broken platform to replenish the concentration";
                            else firstLevelText.text = "+1 extra point when active concentration";
                            return;
                        }
                    }
                    break;
            }
        }
    }
}