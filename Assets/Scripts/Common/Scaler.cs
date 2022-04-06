using DG.Tweening;
using UnityEngine;

namespace Common
{
    public class Scaler : MonoBehaviour
    {
        [SerializeField] private bool enableScalingAnimation;
        [SerializeField, Range(0, 1)] private float minScale;
        [SerializeField, Range(0, 2)] private float maxScale;
        [SerializeField] private float durationScaling;

        private void Start()
        {
            if (enableScalingAnimation)
            {
                transform.localScale = new Vector3(minScale, minScale, minScale);
                ScaleUp();
            }
        }

        private void ScaleUp()
        {
            var scale = transform.DOScale(new Vector3(maxScale, maxScale, maxScale), durationScaling);
            if (scale.onComplete == null) scale.onComplete += ScaleDown;
        }

        private void ScaleDown()
        {
            var scale = transform.DOScale(new Vector3(minScale, minScale, minScale), durationScaling);
            if (scale.onComplete == null) scale.onComplete += ScaleUp;
        }
    }
}