using System.Collections;
using Data.Core.Segments.Content;
using ObjectPool;
using Sound;
using UnityEngine;

namespace Core.Bonuses
{
    public class Acceleration : MonoBehaviour
    {
        private BonusController bonusController;
        private SegmentContentPool segmentContentPool;
        
        private Transform startMarker;
        public Transform endMarker;

        public float speed = 2F;
        private float startTime;
        private float journeyLength;
        private bool ismove = true;
        private Coroutine coroutine;
        private BonusSoundManager bonusSoundManager;
        
        public void BindAudio(BonusSoundManager bonusSoundManager)
        {
            this.bonusSoundManager = bonusSoundManager;
        }
        
        public void Construct(BonusController _bonusController, SegmentContentPool segmentContentPool)
        {
            this.segmentContentPool = segmentContentPool;
            bonusController = _bonusController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            other.GetComponent<Player>().SpawnBonusCollectingEffect();
            bonusController.ActivateBonus(BonusType.Acceleration);
            bonusSoundManager.PlayAccelerationSound();
            segmentContentPool.ReturnObjectToPool(SegmentContent.Acceleration, gameObject);
        }
        
        public void MoveToTargetTransform(Transform _transform)
        {
            startMarker = transform;
            endMarker = _transform;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(Move());
        }
    
        private IEnumerator Move()
        {
            while (startMarker.position != endMarker.position) 
            {
                var distCovered = (Time.time - startTime) * speed;
                var fractionOfJourney = distCovered / journeyLength;
                startMarker.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
                yield return null;
            }
        }
    }
}