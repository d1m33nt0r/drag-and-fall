using System;
using Progress;
using Sound;
using UI.Bonuses;
using UnityEngine;

namespace Core.Bonuses
{
    [Serializable]
    public class TimerBonus
    {
        public BonusType bonusType;
        public Sprite sprite;
        public float timer;
    }

    public class BonusController : MonoBehaviour
    {
        [SerializeField] private BonusSoundManager bonusSoundManager;
        
        [SerializeField] private ProgressController progressController;
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private Player player;
        [SerializeField] private FreeSpeedIncrease freeSpeedIncrease;
        [SerializeField] private AudioSource fireAudioSource;
        
        public int currentShieldLevel;
        public int currentMultiplierLevel;
        public int currentMagnetLevel;
        public int currentAccelerationLevel;
        
        public bool shieldIsActive { get; set; }
        public bool accelerationIsActive { get; set; }
        public bool multiplierIsActive { get; set; }
        public bool magnetIsActive { get; set; }
        
        public TimerBonus[] bonusParams;
        public BonusView[] bonusViews;
        public BonusSlot[] bonusSlots;
        
        public int multiplier = 0;
        
        public float multiplierTimer => 10 + currentMultiplierLevel;
        public float workMultiplierTimer;
        public float shieldTimer => 10 + currentShieldLevel;
        public float workShieldTimer;
        public float magnetTimer => 10 + currentShieldLevel;
        public float workMagnetTimer;
        public int countPlatformsForAcceleration => 10 + currentAccelerationLevel;
        public int workCountPlatformsAcceleration;

        public int concentration;
        
        public TimerBonus GetTimerBonus(BonusType _bonusType)
        {
            for (var i = 0; i < bonusParams.Length; i++)
            {
                if (bonusParams[i].bonusType == _bonusType) return bonusParams[i];
            }

            return null;
        }

        public float GetUpgradedTimer(BonusType bonusType)
        {
            var currentLevel = 0;
                
            switch (bonusType)
            {
                case BonusType.Acceleration:
                    currentLevel = currentAccelerationLevel;
                    break;
                case BonusType.Multiplier:
                    currentLevel = currentMultiplierLevel;
                    break;
                case BonusType.Shield:
                    currentLevel = currentShieldLevel;
                    break;
                case BonusType.Magnet:
                    currentLevel = currentMagnetLevel;
                    break;
            }
            
            for (var i = 0; i < bonusParams.Length; i++)
            {
                if (bonusParams[i].bonusType == bonusType) 
                    return bonusParams[i].timer + Convert.ToSingle(currentLevel);
            }

            return default;
        }

        public void StepAcceleration()
        {
            workCountPlatformsAcceleration -= 1;
            if (workCountPlatformsAcceleration == 0)
            {
                accelerationIsActive = false;
                //platformMover.SetMovementSpeed(2.25f);
            }
        }
        
        private void Start()
        {
            UpdateBonusLevels();
            bonusViews = GetComponentsInChildren<BonusView>();
            DeactivateAllBonuses();
        }

        public void UpdateBonusLevels()
        {
            UpdateAccelerationLevel();
            UpdateShieldLevel();
            UpdateMagnetLevel();
            UpdateMultiplierLevel();
        }

        private void UpdateAccelerationLevel()
        {
            for (var i = 0; i < progressController.upgradeProgress.progressAcceleration.Length; i++)
            {
                if (!progressController.upgradeProgress.progressAcceleration[i])
                {
                    currentAccelerationLevel = i;
                    return;
                }
            }
        }
        
        private void UpdateShieldLevel()
        {
            for (var i = 0; i < progressController.upgradeProgress.progressShield.Length; i++)
            {
                if (!progressController.upgradeProgress.progressShield[i])
                {
                    currentShieldLevel = i;
                    return;
                }
            }
        }
        
        private void UpdateMagnetLevel()
        {
            for (var i = 0; i < progressController.upgradeProgress.progressMagnet.Length; i++)
            {
                if (!progressController.upgradeProgress.progressMagnet[i])
                {
                    currentMagnetLevel = i;
                    return;
                }
            }
        }
        
        private void UpdateMultiplierLevel()
        {
            for (var i = 0; i < progressController.upgradeProgress.progressMultiplier.Length; i++)
            {
                if (!progressController.upgradeProgress.progressMultiplier[i])
                {
                    currentMultiplierLevel = i;
                    return;
                }
            }
        }

        public void DeactivateAllBonuses()
        {
            for (var i = 0; i < bonusViews.Length; i++)
                bonusViews[i].Deactivate();

            shieldIsActive = false;
            accelerationIsActive = false;
            multiplierIsActive = false;
            magnetIsActive = false;
        }

        public void ActivateBonus(BonusType bonusType)
        {
            switch (bonusType)
            {
                case BonusType.Acceleration:
                    if (platformMover.platformMovementSpeed < 6)
                    {
                        player.SetActiveFireEffect(true);
                        player.MaximumFire();
                    }
                    platformMover.SetMovementSpeed(8f);
                    if (!fireAudioSource.isPlaying) fireAudioSource.Play();
                    workCountPlatformsAcceleration = countPlatformsForAcceleration;
                    accelerationIsActive = true;
                    break;
                case BonusType.Multiplier:
                    if (TryGetActivatedBonus(bonusType, out var bonusView2))
                    {
                        if (multiplier < 32) multiplier = multiplier * 2;
                        bonusView2.ResetTimer();
                    }
                    else
                    {
                        if (FindEmptyBonusSlot(out var emptyView))
                        {
                            multiplier = 2;
                            emptyView.SetUp(GetBonusViewByType(BonusType.Multiplier));
                        }
                    }

                    multiplierIsActive = true;
                    break;
                case BonusType.Shield:
                    if (TryGetActivatedBonus(bonusType, out var bonusView))
                    {
                        bonusView.ResetTimer();
                    }
                    else
                    {
                        if (FindEmptyBonusSlot(out var emptyView))
                        {
                            emptyView.SetUp(GetBonusViewByType(BonusType.Shield));
                        }
                    }

                    shieldIsActive = true;
                    break;
                case BonusType.Magnet:
                    if (TryGetActivatedBonus(bonusType, out var bonusView3))
                    {
                        bonusView3.ResetTimer();
                    }
                    else
                    {
                        if (FindEmptyBonusSlot(out var emptyView))
                        {
                            emptyView.SetUp(GetBonusViewByType(BonusType.Magnet));
                        }
                    }

                    magnetIsActive = true;
                    break;
            }
        }

        private BonusView GetBonusViewByType(BonusType bonusType)
        {
            for (var i = 0; i < bonusViews.Length; i++)
            {
                if (bonusViews[i].bonusType == bonusType)
                {
                    bonusViews[i].Construct();
                    return bonusViews[i]; 
                }
            }

            return null;
        }
        
        private bool TryGetActivatedBonus(BonusType bonusType, out BonusView bonusView)
        {
            for (var i = 0; i < bonusViews.Length; i++)
            {
                if (bonusViews[i].isActive && bonusViews[i].bonusType == bonusType)
                {
                    bonusView = bonusViews[i];
                    return true;
                }
            }

            bonusView = default;
            return false;
        }

        private bool FindEmptyBonusSlot(out BonusSlot bonusSlot)
        {
            for (var i = 0; i < bonusSlots.Length; i++)
            {
                if (!bonusSlots[i].contained)
                {
                    bonusSlot = bonusSlots[i];
                    return true;
                }
            }

            bonusSlot = default;
            return false;
        }

        public void DeactivateBonus(BonusType bonusType)
        {
            for (var i = 0; i < bonusViews.Length; i++)
            {
                if (bonusViews[i].bonusType == bonusType)
                    bonusViews[i].Deactivate();
            }

            switch (bonusType)
            {
                case BonusType.Acceleration:
                    accelerationIsActive = false;
                    bonusSoundManager.DeactivateBonusSound();
                    break;
                case BonusType.Multiplier:
                    multiplierIsActive = false;
                    bonusSoundManager.DeactivateBonusSound();
                    break;
                case BonusType.Shield:
                    shieldIsActive = false;
                    bonusSoundManager.DeactivateBonusSound();
                    break;
                case BonusType.Magnet:
                    magnetIsActive = false;
                    bonusSoundManager.DeactivateBonusSound();
                    break;
            }
        }
    }
}