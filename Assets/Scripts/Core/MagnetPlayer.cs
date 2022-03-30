using Core.Bonuses;
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
            }

            if (other.TryGetComponent<Crystal>(out var crystal))
            {
                crystal.MoveToTargetTransform(transform);
            }

            if (other.TryGetComponent<Acceleration>(out var acceleration))
            {
                acceleration.MoveToTargetTransform(transform);
            }
            
            if (other.TryGetComponent<Key>(out var key))
            {
                key.MoveToTargetTransform(transform);
            }
            
            if (other.TryGetComponent<Magnet>(out var magnet))
            {
                magnet.MoveToTargetTransform(transform);
            }
            
            if (other.TryGetComponent<Multiplier>(out var multiplier))
            {
                multiplier.MoveToTargetTransform(transform);
            }
            
            if (other.TryGetComponent<Shield>(out var shield))
            {
                shield.MoveToTargetTransform(transform);
            }
        }
    }
}