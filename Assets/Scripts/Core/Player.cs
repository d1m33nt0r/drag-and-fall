using Data.Core.Segments;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject fallingTrail;
        [SerializeField] private GameObject trail;
        [SerializeField] private GameManager gameManager;
        
        private bool triggerStay;

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

        public void SetTriggerStay(bool _value)
        {
            triggerStay = _value;
        }

        private void Update()
        {
            if (!gameManager.gameStarted) return;
            if (triggerStay) return;
            
            var position = transform.position;
            var centerRay = new Ray(position, Vector3.down);
            
            if (Physics.Raycast(centerRay, out var centerHit, 0.101f))
            {
                var segment = centerHit.collider.GetComponent<Segment>();

                switch (segment.segmentData.segmentType)
                {
                    case SegmentType.Ground:
                        triggerStay = true;
                        PlayBounceAnim();
                        EnableTrail();
                        DisableFallingTrail();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BounceTrigger")) triggerStay = false;
        }
    }
}

