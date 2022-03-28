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
        
        private bool destroy;
        public Action platformDestroyed;
        public int countTouches = 0;

        public PatternData patternData;
        private Player player;
        private PlatformMover platformMover;
        [SerializeField] private Segment[] segments;
        private GainScore gainScore;
        private SegmentContentPool segmentContentPool;
        private TubeMover tubeMover;
        
        public void Initialize(int _segmentsCount, PatternData patternData, PlatformMover platformMover, 
            Player _player, BonusController _bonusController, GainScore gainScore, SegmentContentPool segmentContentPool,
            TubeMover _tubeMover)
        {
            this.segmentContentPool = segmentContentPool;
            this.platformMover = platformMover;
            this.gainScore = gainScore;
            this.patternData = patternData;
            this.tubeMover = _tubeMover;
            for (var i = 1; i <= patternData.segmentsData.Length; i++)
            {
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
                }
            }

            if (countTouches == 2)
            {
                for (var i = 0; i < 12; i++)
                {
                    segments[i].GetComponent<Segment>().ChangeColor(2);
                }
            }
            
            if (countTouches == 2) resetConcentraion?.Invoke();
            if (countTouches == 3) DestroyPlatform(true);
        }
        
        public void DestroyPlatform(bool platformsIsMoving)
        {
            for (var i = 0; i < 12; i++)
            {
                segments[i].GetComponent<Segment>().ChangeColor(3);
            }
            
            gainScore.SetText(1);
            gainScore.Animate();
            
            if (platformMover.isLevelMode && patternData.isLast) platformMover.FinishLevel(platformMover.gameManager.gameMode.levelMode.level);
            platformDestroyed?.Invoke();

            if (platformsIsMoving)
            {
                tubeMover.MoveTube();
                platformMover.MovePlatforms();
            }
            
            increaseConcentraion?.Invoke();
            for (var i = 0; i < segments.Length; i++)
            {
                //segments[i].ReturnTouchEffectToPool();
                segments[i].ReturnSegmentContentToPool();
            }
            BreakDownPlatform();
            player.SetTriggerStay(false);
            //Destroy(gameObject);
        }

        public void DestroyAfterBreakAnimation()
        {
            
            Destroy(gameObject);
        }

        private void BreakDownPlatform()
        {
            GetComponent<Animator>().Play("Break");
            for (var i = 0; i < segments.Length; i++)
            {
                segments[i].gameObject.GetComponent<MeshCollider>().enabled = false;
                /*var rb = segments[i].gameObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddForce(Random.Range(-3, 3), Random.Range(0, 10), Random.Range(2, 5), ForceMode.Impulse);*/
            }
            
        }
    }
}