using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Concentration : MonoBehaviour
    {
        public bool isActive => platformCounter > Constants.Concentration.CONCENTRATION_THRESHOLD;
        public Slider slider;
        public int platformCounter { get; private set; }

        public void IncreaseConcentration()
        {
            platformCounter++;
            if (isActive) return;
            slider.value = platformCounter;
        }

        public void Reset()
        {
            platformCounter = 0;
            slider.value = platformCounter;
        }
    }
}