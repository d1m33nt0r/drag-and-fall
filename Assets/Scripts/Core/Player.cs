using Data.Core.Segments;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject fallingTrail;
        [SerializeField] private GameObject trail;
        
        public void PlayIdleAnim()
        {
            animator.Play("Idle");
        }

        public void DisableTrail()
        {
            trail.SetActive(false);
        }

        public void EnableTrail()
        {
            trail.SetActive(trail);
        }
        
        public void PlayBounceAnim()
        {
            animator.Play("Bounce");
        }
        
        public void EnableFallingTrail()
        {
            fallingTrail.SetActive(true);
        }

        public void DisableFallingTrail()
        {
            fallingTrail.SetActive(false);
        }

        public void OnCollisionEnter(Collision other)
        {
            var segment = other.collider.GetComponent<Segment>();

            switch (segment.segmentData.segmentType)
            {
                case SegmentType.Ground:
                    segment.IncreasePlatformTouchCounter();
                    break;
                case SegmentType.Hole:
                    segment.DestroyPlatform();
                    break;
                case SegmentType.Let:
                    
                    break;
            }
        }
    }
}