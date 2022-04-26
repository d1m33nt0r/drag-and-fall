using System.Collections;
using System.Threading.Tasks;
using Core;
using Core.Bonuses;
using Cysharp.Threading.Tasks;
using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bonuses
{
    public class BonusView : MonoBehaviour
    {
        
        [SerializeField] private BonusSoundManager bonusSoundManager;
        public Player player;
        public BonusController bonusController;
        
        public bool isActive { get; private set; }
        public BonusType bonusType;
        public Coroutine coroutine;
        private float defaultTimerValue;
        [SerializeField] private Text multiplierText;
        [SerializeField] private GameObject shieldPlayerEffect;

        public RectTransform bonusIconPanel;
        public RectTransform bonusIcon;
        public Slider timer;
        public RectTransform fillArea;
        public Text timerText;

        private WaitForSeconds waitForSeconds;
        
        public void Construct()
        {
            bonusIconPanel.gameObject.SetActive(true);
            if (bonusIcon != null) bonusIcon.gameObject.SetActive(true);
            timer.gameObject.SetActive(true);
            fillArea.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            
            
            isActive = true;
            defaultTimerValue = bonusController.GetUpgradedTimer(bonusType);
            if (bonusType == BonusType.Multiplier)
            {
                multiplierText.gameObject.SetActive(true);
                multiplierText.text = "x" + bonusController.multiplier;
            }
            timer.maxValue = defaultTimerValue;
            timer.value = defaultTimerValue;
            SetActive(true);
            //if (coroutine != null) StopCoroutine(coroutine);
            //coroutine = StartCoroutine(Timer(0.1f));
            //AsyncTimer(100);
            UniTimer(100);
        }

        public void ResetTimer()
        {
            timer.value = defaultTimerValue;
            if (bonusType == BonusType.Multiplier) multiplierText.text = "x" + bonusController.multiplier;
        }
            
        
        private IEnumerator Timer(float interval)
        {
            if (waitForSeconds == null) waitForSeconds = new WaitForSeconds(interval);
            while (timer.value > 0)
            {
                SetTimerValue(timer.value -= interval);
                var timerValue = (int) timer.value;
                timerText.text = timerValue.ToString();
                yield return waitForSeconds;
            }
            
            Deactivate();
        }

        private async UniTask UniTimer(int interval)
        {
            while (timer.value > 0)
            {
                SetTimerValue(timer.value -= interval * 0.001f);
                var timerValue = (int) timer.value;
                timerText.text = timerValue.ToString();
                await UniTask.Delay(interval);
            }
        }
        
        private async void AsyncTimer(int interval)
        {
            while (timer.value > 0)
            {
                SetTimerValue(timer.value -= interval * 0.001f);
                var timerValue = (int) timer.value;
                timerText.text = timerValue.ToString();
                await Task.Delay(interval);
            }
            
            Deactivate();
        }

        public void Deactivate()
        {
            bonusIconPanel.gameObject.SetActive(false);
            if (bonusIcon != null) bonusIcon.gameObject.SetActive(false);
            timer.gameObject.SetActive(false);
            fillArea.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);

            //if (coroutine != null) StopCoroutine(coroutine);
            if (isActive) bonusSoundManager.DeactivateBonusSound();
            //transform.GetChild(0).GetComponent<Image>().sprite = null;
            if (bonusType == BonusType.Multiplier)
            {
                bonusController.multiplier = 0;
                bonusController.multiplierIsActive = false;
                multiplierText.gameObject.SetActive(false);
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