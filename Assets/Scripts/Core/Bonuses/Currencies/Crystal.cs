using Common;
using Data.Core.Segments.Content;
using ObjectPool;
using Sound;
using UnityEngine;

namespace Core
{
    public class Crystal : MonoBehaviour
    {
        private int count = 1;
        
        private Transform startMarker;
        public Transform endMarker;

        private bool isMoving;
        public float speed = 1.0F;
        private float startTime;
        private float journeyLength;
        private bool ismove = true;
        private SegmentContentPool segmentContentPool;
        private Coroutine coroutine;

        private CrystalSound crystalSound;
        
        public void BindAudio(CrystalSound crystalSound)
        {
            this.crystalSound = crystalSound;
        }
        private void SetMovingFalse()
        {
            isMoving = false;
        }
        
        public void Construct(SegmentContentPool segmentContentPool)
        {
            this.segmentContentPool = segmentContentPool;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tags.PLAYER))
            {
                var player = other.GetComponent<Player>();
                player.SpawnCrystalCollectingEffect();
                player.CollectCrystal(count);
                crystalSound.Play();
                SetMovingFalse();
                segmentContentPool.ReturnObjectToPool(SegmentContent.Crystal, gameObject);
            }
        }
        
        public void MoveToTargetTransform(Transform _transform)
        {
            Transform transform1;
            (transform1 = transform).SetParent(null);
            startMarker = transform1;
            endMarker = _transform;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
            isMoving = true;
        }
    
        private void Update()
        {
            if (!isMoving) return;
            
            var distCovered = (Time.time - startTime) * speed;
            var fractionOfJourney = distCovered / journeyLength;
            startMarker.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        }
    }
}