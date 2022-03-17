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
        private float defaultTimerValue;
        public void SetIndex(int index)
        {
            this.index = index;
        }

        public void Construct(BonusType bonusType)
        {
            this.bonusType = bonusType;
            isActive = true;
            transform.GetChild(0).GetComponent<Image>().sprite = bonusController.GetTimerBonus(bonusType).sprite;
            defaultTimerValue = bonusController.GetTimerBonus(bonusType).timer;
            timer.maxValue = defaultTimerValue;
            timer.value = defaultTimerValue;
            SetActive(true);
            coroutine = StartCoroutine(Timer(0.1f));
        }

        public void ResetTimer()
        { 
            timer.value = defaultTimerValue;
        }

        private IEnumerator Timer(float interval)
        {
            while (timer.value > 0)
            {
                SetTimerValue(timer.value -= interval); ;
                yield return new WaitForSeconds(interval);
            }
            
            Deactivate();
        }

        public void Deactivate()
        {
            if (coroutine != null) StopCoroutine(coroutine);
            transform.GetChild(0).GetComponent<Image>().sprite = null;
            timer.value = defaultTimerValue;
            SetActive(false);
        }
        
        public void SetActive(bool value)
        {
            isActive = value;
            gameObject.SetActive(value);
            if (!value) bonusType = BonusType.None;
        }
        private void SetTimerValue(float time)
        {
            timer.value = time;
        }
    }
}