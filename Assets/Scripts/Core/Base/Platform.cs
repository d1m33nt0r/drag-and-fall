using System;
using Core.Bonuses;
using Data.Core;
using Data.Core.Segments;
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
        
        public Action platformDestroyed;
        public int countTouches = 0;

        public PatternData patternData;
        private Player player;
        private Tube tube;
        private Segment[] segments;
        private float angle;
        private GainScore gainScore;
        
        public void Initialize(int _segmentsCount, PatternData patternData, Tube _tube, Player _player, BonusController _bonusController, GainScore gainScore)
        {
            angle = 360 / _segmentsCount;
            segments = new Segment[_segmentsCount];
            tube = _tube;

            this.gainScore = gainScore;
            this.patternData = patternData;
            var position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

            for (var i = 1; i <= patternData.segmentsData.Length; i++)
            {
                segments[i - 1] = Instantiate(segmentPrefab, position, Quaternion.AngleAxis(angle * i, Vector3.up), transform);
                segments[i - 1].Initialize(patternData.segmentsData[i - 1], _tube, this, _bonusController);
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
                    if (segments[i].segmentData.segmentType == SegmentType.Ground)
                    {
                        segments[i].GetComponent<MeshRenderer>().material.color = Color.blue;
                    }
                }
            }

            if (countTouches == 2)
            {
                for (var i = 0; i < 12; i++)
                {
                    if (segments[i].segmentData.segmentType == SegmentType.Ground)
                    {
                        segments[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    }
                }
            }
            
            if (countTouches == 2) resetConcentraion?.Invoke();
            if (countTouches == 3) DestroyPlatform();
        }
        
        public void DestroyPlatform()
        {
            gainScore.SetText(1);
            gainScore.Animate();
            
            if (tube.isLevelMode && patternData.isLast) tube.FinishLevel(tube.gameManager.gameMode.levelMode.level);
            platformDestroyed?.Invoke();
            player.SetTriggerStay(false);
            tube.MovePlatforms();
            increaseConcentraion?.Invoke();
            Destroy(gameObject);
        }
        
        public void BreakDownPlatform()
        {
            Rigidbody[] rb = new Rigidbody[segments.Length];

            for(var i = 0; i < segments.Length; i++)
            {
                segments[i].gameObject.GetComponent<MeshCollider>().enabled = false;
                rb[i] = segments[i].gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            }

            for(var i = 0; i < segments.Length; i++)
                rb[i].AddForce(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(2, 5), ForceMode.Impulse);
        }
    }
}