using System;
using Core.Bonuses;
using Data.Core;
using ObjectPool;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class Platform : MonoBehaviour
    {
        public event Action resetConcentraion;
        public event Action increaseConcentraion;
        
        [SerializeField] private Segment segmentPrefab;

        private bool destroy;
        public Action platformDestroyed;
        public int countTouches = 0;

        public PatternData patternData;
        private Player player;
        private PlatformMover platformMover;
        private Segment[] segments;
        private float angle;
        private GainScore gainScore;
        private SegmentContentPool segmentContentPool;
        
        public void Initialize(int _segmentsCount, PatternData patternData, PlatformMover platformMover, 
            Player _player, BonusController _bonusController, GainScore gainScore, SegmentContentPool segmentContentPool)
        {
            this.segmentContentPool = segmentContentPool;
            angle = 360 / _segmentsCount;
            segments = new Segment[_segmentsCount];
            this.platformMover = platformMover;

            this.gainScore = gainScore;
            this.patternData = patternData;
            var position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

            for (var i = 1; i <= patternData.segmentsData.Length; i++)
            {
                segments[i - 1] = Instantiate(segmentPrefab, position, Quaternion.AngleAxis(angle * i, Vector3.up), transform);
                segments[i - 1].Initialize(patternData.segmentsData[i - 1], platformMover, this, _bonusController, segmentContentPool);
            }

            platformDestroyed += _player.SetFallingTrailState;
            player = _player;
        }

        public void IncreaseTouchCounter()
        {
            countTouches++;
            
            if (countTouches == 1)
            {
                for (var i = 0; i < 12; i++)
                {
                    segments[i].GetComponent<Segment>().ChangeColor(1);
                    /*if (segments[i].segmentData.segmentType == SegmentType.Ground)
                    {
                        segments[i].GetComponent<MeshRenderer>().material.color = Color.blue;
                    }*/
                }
            }

            if (countTouches == 2)
            {
                for (var i = 0; i < 12; i++)
                {
                    segments[i].GetComponent<Segment>().ChangeColor(2);
                    /*if (segments[i].segmentData.segmentType == SegmentType.Ground)
                    {
                        segments[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    }*/
                }
            }
            
            if (countTouches == 2) resetConcentraion?.Invoke();
            if (countTouches == 3) DestroyPlatform();
        }
        
        public void DestroyPlatform()
        {
            for (var i = 0; i < 12; i++)
            {
                segments[i].GetComponent<Segment>().ChangeColor(3);
                /*if (segments[i].segmentData.segmentType == SegmentType.Ground)
                {
                    segments[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
                }*/
            }
            
            gainScore.SetText(1);
            gainScore.Animate();
            
            if (platformMover.isLevelMode && patternData.isLast) platformMover.FinishLevel(platformMover.gameManager.gameMode.levelMode.level);
            platformDestroyed?.Invoke();
            
            platformMover.MovePlatforms();
            increaseConcentraion?.Invoke();
            for (var i = 0; i < segments.Length; i++)
            {
                segments[i].ReturnSegmentContentToPool();
            }
            BreakDownPlatform();
            //Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            player.SetTriggerStay(false);
        }

        public void BreakDownPlatform()
        {
            for(var i = 0; i < segments.Length; i++)
            {
                segments[i].gameObject.GetComponent<MeshCollider>().enabled = false;
                var rb = segments[i].gameObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddForce(Random.Range(-3, 3), Random.Range(0, 10), Random.Range(2, 5), ForceMode.Impulse);
            }
            player.SetTriggerStay(false);
        }
    }
}