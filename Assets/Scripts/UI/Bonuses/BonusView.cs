using System.Collections;
using Core.Bonuses;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bonuses
{
    public class BonusView : MonoBehaviour
    {
        public BonusController bonusController;
        public Slider timer;
        public bool isActive { get; private set; }
        public int index;
        public BonusType bonusType { get; private set; }
        public Coroutine coroutine;

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public void Construct(BonusType bonusType)
        {
            this.bonusType = bonusType;
            isActive = true;
            transform.GetChild(0).GetComponent<Image>().sprite = bonusController.GetTimerBonus(bonusType).sprite;
            timer.maxValue = bonusController.GetTimerBonus(bonusType).timer;
            timer.value = bonusController.GetTimerBonus(bonusType).timer;
            SetActive(true);
            coroutine = StartCoroutine(Timer(0.1f));
        }

        public void ResetTimer() => timer.value = bonusController.GetTimerBonus(bonusType).timer;
        
        private IEnumerator Timer(float interval)
        {
            while (bonusController.GetTimerBonus(bonusType).timer > 0)
            {
                bonusController.GetTimerBonus(bonusType).timer -= interval;
                SetTimerValue(bonusController.GetTimerBonus(bonusType).timer);
                yield return new WaitForSeconds(interval);
            }
            
            Deactivate();
        }

        public void Deactivate()
        {
            if (coroutine != null) StopCoroutine(coroutine);
            isActive = default;
            bonusType = BonusType.None;
            transform.GetChild(0).GetComponent<Image>().sprite = default;
            timer.value = default;
            timer.maxValue = default;
            SetActive(default);
        }
        
        public void SetActive(bool value)
        {
            isActive = value;
            gameObject.SetActive(value);
        }

        private void SetTimerValue(float time)
        {
            timer.value = time;
        }
    }
}