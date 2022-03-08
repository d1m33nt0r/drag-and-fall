using System;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.FpsCounter
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private Text fpsDisplay;
        
        private void Update()
        {
            float fps = 1 / Time.unscaledDeltaTime;
            fpsDisplay.text = "" + Convert.ToInt16(fps);;
        }
    }
}