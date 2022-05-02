using System.Collections;
using Common;
using Cysharp.Threading.Tasks;
using Data.Core.Segments.Content;
using ObjectPool;
using Sound;
using UnityEngine;

namespace Core
{
    public class Coin : MonoBehaviour
    {
        private int count = 1;
        
        private Transform startMarker;
        public Transform endMarker;

        public float speed = 1.0F;
        private float startTime;
        private float journeyLength;
        private bool ismove = true;
        private SegmentContentPool segmentContentPool;
        private Coroutine coroutine;
        private CoinSound coinSound;

        public void BindAudio(CoinSound coinSound)
        {
            this.coinSound = coinSound;
        }
        
        public void Construct(SegmentContentPool segmentContentPool)
        {
            this.segmentContentPool = segmentContentPool;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.TAGS.PLAYER))
            {
                other.GetComponent<Player>().SpawnCoinCollectingEffect();
                other.GetComponent<Player>().CollectCoin(count);
                coinSound.Play();
                segmentContentPool.ReturnObjectToPool(SegmentContent.Coin, gameObject);
            }
        }
        
        public void MoveToTargetTransform(Transform _transform)
        {
            startMarker = transform;
            endMarker = _transform;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
            Move();
        }
    
        private async UniTask Move()
        {
            while (startMarker.position != endMarker.position) 
            {
                var distCovered = (Time.time - startTime) * speed;
                var fractionOfJourney = distCovered / journeyLength;
                startMarker.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
                await UniTask.Yield();
            }
        }
    }
}