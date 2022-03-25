using System.Collections;
using Core;
using Core.Bonuses;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bonuses
{
    public class BonusView : MonoBehaviour
    {
        public Player player;
        public BonusController bonusController;
        public Slider timer;
        public bool isActive { get; private set; }
        public BonusType bonusType;
        public Coroutine coroutine;
        private float defaultTimerValue;
        [SerializeField] private GameObject shieldPlayerEffect;
        public void Construct()
        {
            isActive = true;
            defaultTimerValue = bonusController.GetUpgradedTimer(bonusType);
            timer.maxValue = defaultTimerValue;
            timer.value = defaultTimerValue;
            SetActive(true);
            coroutine = StartCoroutine(Timer(0.1f));
        }

        public void ResetTimer() =>
            timer.value = defaultTimerValue;

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
            if (bonusType == BonusType.Multiplier) bonusController.multiplier = 0;
            timer.value = defaultTimerValue;
            if (bonusType == BonusType.Magnet) player.SetActiveMagnet(false);
            if (transform.parent.GetComponent<BonusSlot>())
                transform.parent.GetComponent<BonusSlot>().SetDown(this);
            SetActive(false);
        }
        
        public void SetActive(bool value)
        {
            isActive = value;
            gameObject.SetActive(value);
            if (bonusType == BonusType.Shield)
                shieldPlayerEffect.SetActive(value);
        }
        
        private void SetTimerValue(float time) =>
            timer.value = time;
    }
}