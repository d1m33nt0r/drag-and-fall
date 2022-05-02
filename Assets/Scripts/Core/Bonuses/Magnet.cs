using System.Collections;
using Common;
using Cysharp.Threading.Tasks;
using Data.Core.Segments.Content;
using ObjectPool;
using Sound;
using UnityEngine;

namespace Core.Bonuses
{
    public class Magnet : MonoBehaviour
    {
        private BonusController bonusController;
        private SegmentContentPool segmentContentPool;

        private Transform startMarker;
        public Transform endMarker;

        private bool isMoving;
        public float speed = 1.0F;
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
            bonusController = _bonusController;
            this.segmentContentPool = segmentContentPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.Tags.PLAYER)) return;

            other.GetComponent<Player>().SetActiveMagnet(true);
            other.GetComponent<Player>().SpawnBonusCollectingEffect();
            bonusController.ActivateBonus(BonusType.Magnet);
            bonusSoundManager.PlayMagnetSound();
            SetMovingFalse();
            segmentContentPool.ReturnObjectToPool(SegmentContent.Magnet, gameObject);
        }
        
        private void SetMovingFalse()
        {
            isMoving = false;
        }

        public void MoveToTargetTransform(Transform _transform)
        {
            startMarker = transform;
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