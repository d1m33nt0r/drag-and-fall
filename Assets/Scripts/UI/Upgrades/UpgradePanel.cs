using Core.Bonuses;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Upgrades
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private ProgressController progressController;
        [SerializeField] private BonusType bonusType;
        
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
                }
            }
        }

        public void UpgradeBonus()
        {
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
                            return;
                        }
                    }
                    break;
            }
        }
    }
}