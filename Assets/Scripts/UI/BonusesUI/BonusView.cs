using System.Collections;
using Core;
using Core.Bonuses;
using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bonuses
{
    public class BonusView : MonoBehaviour
    {
        [SerializeField] private Text timerText;
        [SerializeField] private BonusSoundManager bonusSoundManager;
        
        public Player player;
        public BonusController bonusController;
        public Slider timer;
        public bool isActive { get; private set; }
        public BonusType bonusType;
        public Coroutine coroutine;
        private float defaultTimerValue;
        [SerializeField] private Text multiplierText;
        
        [SerializeField] private GameObject shieldPlayerEffect;
        public void Construct()
        {
            isActive = true;
            defaultTimerValue = bonusController.GetUpgradedTimer(bonusType);
            if (bonusType == BonusType.Multiplier) multiplierText.text = "x" + bonusController.multiplier;
            timer.maxValue = defaultTimerValue;
            timer.value = defaultTimerValue;
            SetActive(true);
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(Timer(0.1f));
        }

        public void ResetTimer()
        {
            timer.value = defaultTimerValue;
            if (bonusType == BonusType.Multiplier) multiplierText.text = "x" + bonusController.multiplier;
        }
            
        
        private IEnumerator Timer(float interval)
        {
            while (timer.value > 0)
            {
                SetTimerValue(timer.value -= interval);
                var timerValue = (int) timer.value;
                timerText.text = timerValue.ToString();
                yield return new WaitForSeconds(interval);
            }
            
            Deactivate();
        }

        public void Deactivate()
        {
            if (coroutine != null) StopCoroutine(coroutine);
            if (isActive) bonusSoundManager.DeactivateBonusSound();
            transform.GetChild(0).GetComponent<Image>().sprite = null;
            if (bonusType == BonusType.Multiplier)
            {
                bonusController.multiplier = 0;
                bonusController.multiplierIsActive = false;
            }
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