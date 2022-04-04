using UnityEngine;

namespace Core
{
    public class PlayerPositionObserver : MonoBehaviour
    {
        [SerializeField] private Transform shieldEffect;
        [SerializeField] private Transform magnetEffect;

        private void Update()
        {
            shieldEffect.transform.position = transform.position;
            magnetEffect.transform.position = transform.position;
        }
    }
}