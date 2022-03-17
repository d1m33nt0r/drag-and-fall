using UnityEngine;

namespace Core
{
    public class MagnetPlayer : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("SegmentContent")) return;

         
            if (other.TryGetComponent<Coin>(out var coin))
            {
                coin.MoveToTargetTransform(transform);
                return;
            }

            if (other.TryGetComponent<Crystal>(out var crystal))
            {
                crystal.MoveToTargetTransform(transform);
            }
        }
    }
}