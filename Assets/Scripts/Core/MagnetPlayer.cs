using Core.Bonuses;
using UnityEngine;

namespace Core
{
    public class MagnetPlayer : MonoBehaviour
    {
        private const string SEGMENT_CONTENT = "SegmentContent";

        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(SEGMENT_CONTENT)) return;

            if (other.TryGetComponent<Coin>(out var coin))
            {
                coin.transform.SetParent(null);
                coin.MoveToTargetTransform(transform);
            }

            if (other.TryGetComponent<Crystal>(out var crystal))
            {
                crystal.transform.SetParent(null);
                crystal.MoveToTargetTransform(transform);
            }

            if (other.TryGetComponent<Acceleration>(out var acceleration))
            {
                acceleration.transform.SetParent(null);
                acceleration.MoveToTargetTransform(transform);
            }
            
            if (other.TryGetComponent<Key>(out var key))
            {
                key.transform.SetParent(null);
                key.MoveToTargetTransform(transform);
            }
            
            if (other.TryGetComponent<Magnet>(out var magnet))
            {
                magnet.transform.SetParent(null);
                magnet.MoveToTargetTransform(transform);
            }
            
            if (other.TryGetComponent<Multiplier>(out var multiplier))
            {
                multiplier.transform.SetParent(null);
                multiplier.MoveToTargetTransform(transform);
            }
            
            if (other.TryGetComponent<Shield>(out var shield))
            {
                shield.transform.SetParent(null);
                shield.MoveToTargetTransform(transform);
            }
        }
    }
}