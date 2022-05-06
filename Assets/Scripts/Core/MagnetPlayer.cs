using System;
using Core.Bonuses;
using UnityEngine;

namespace Core
{
    public class MagnetPlayer : MonoBehaviour
    {
        private const string SEGMENT_CONTENT = "SegmentContent";
        private Transform mtransform;

        private void Start()
        {
            mtransform = GetComponent<Transform>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(SEGMENT_CONTENT)) return;

            if (other.TryGetComponent<Coin>(out var coin))
            {
                coin.MoveToTargetTransform(mtransform);
                return;
            }

            if (other.TryGetComponent<Crystal>(out var crystal))
            {
                crystal.MoveToTargetTransform(mtransform);
                return;
            }

            if (other.TryGetComponent<Acceleration>(out var acceleration))
            {
                acceleration.MoveToTargetTransform(mtransform);
                return;
            }
            
            if (other.TryGetComponent<Key>(out var key))
            {
                key.MoveToTargetTransform(mtransform);
                return;
            }
            
            if (other.TryGetComponent<Magnet>(out var magnet))
            {
                magnet.MoveToTargetTransform(mtransform);
                return;
            }
            
            if (other.TryGetComponent<Multiplier>(out var multiplier))
            {
                multiplier.MoveToTargetTransform(mtransform);
                return;
            }
            
            if (other.TryGetComponent<Shield>(out var shield))
            {
                shield.MoveToTargetTransform(mtransform);
            }
        }
    }
}