using System;
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
        [SerializeField] private Tube tube;
        public bool shieldIsActive { get; private set; }
        public bool accelerationIsActive { get; private set; }
        public bool multiplierIsActive { get; private set; }

        public TimerBonus[] bonusParams;
        public BonusView[] bonusViews;

        public int countPlatformsForAcceleration = 12;
        
        public TimerBonus GetTimerBonus(BonusType _bonusType)
        {
            for (var i = 0; i < bonusParams.Length; i++)
            {
                if (bonusParams[i].bonusType == _bonusType) return bonusParams[i];
            }

            return null;
        }

        public void StepAcceleration()
        {
            countPlatformsForAcceleration -= 1;
            if (countPlatformsForAcceleration == 0)
            {
                accelerationIsActive = false;
                countPlatformsForAcceleration = 12;
                tube.SetMovementSpeed(2.25f);
            }
        }
        
        private void Start()
        {
            bonusViews = GetComponentsInChildren<BonusView>();
            for (var i = 0; i < bonusViews.Length; i++)
                bonusViews[i].SetIndex(i);

            DeactivateAllBonuses();
        }

        public void DeactivateAllBonuses()
        {
            for (var i = 0; i < bonusViews.Length; i++)
                bonusViews[i].Deactivate();

            shieldIsActive = false;
            accelerationIsActive = false;
            multiplierIsActive = false;
        }

        public void ActivateBonus(BonusType bonusType)
        {
            switch (bonusType)
            {
                case BonusType.Acceleration:
                    tube.SetMovementSpeed(5);

                    accelerationIsActive = true;
                    break;
                case BonusType.Multiplier:
                    if (TryGetActivatedBonus(bonusType, out var bonusView2))
                    {
                        bonusView2.ResetTimer();
                    }
                    else
                    {
                        if (FindEmptyBonusSlot(out var emptyView))
                            emptyView.Construct(bonusType);
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
                            emptyView.Construct(bonusType);
                    }

                    shieldIsActive = true;
                    break;
            }
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

        private bool FindEmptyBonusSlot(out BonusView bonusView)
        {
            for (var i = 0; i < bonusViews.Length; i++)
            {
                if (!bonusViews[i].isActive)
                {
                    bonusView = bonusViews[i];
                    return true;
                }
            }

            bonusView = default;
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
                case BonusType.Multiplier:
                    multiplierIsActive = false;
                    break;
                case BonusType.Shield:
                    shieldIsActive = false;
                    break;
            }
        }
    }
}