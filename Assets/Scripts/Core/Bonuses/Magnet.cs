using System.Collections;
using Data.Core.Segments.Content;
using ObjectPool;
using UnityEngine;

namespace Core.Bonuses
{
    public class Magnet : MonoBehaviour
    {
        private BonusController bonusController;
        private SegmentContentPool segmentContentPool;
        
        private Transform startMarker;
        public Transform endMarker;

        public float speed = 1.0F;
        private float startTime;
        private float journeyLength;
        private bool ismove = true;
        private Coroutine coroutine;
        
        public void Construct(BonusController _bonusController, SegmentContentPool segmentContentPool)
        {
            bonusController = _bonusController;
            this.segmentContentPool = segmentContentPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            other.GetComponent<Player>().SetActiveMagnet(true);
            other.GetComponent<Player>().SpawnBonusCollectingEffect();
            bonusController.ActivateBonus(BonusType.Magnet);

            segmentContentPool.ReturnObjectToPool(SegmentContent.Magnet, gameObject);
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