using System;
using Data.Core;
using UnityEngine;

namespace Core
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Segment segmentPrefab;
        
        public Action platformDestroyed;
        public int countTouches = 0;
        
        private Segment[] segments;
        private float angle;
        
        public void Initialize(int _segmentsCount, PatternData patternData, Tube _tube, Player _player)
        {
            angle = 360 / _segmentsCount;
            segments = new Segment[_segmentsCount];
            var position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

            for (var i = 1; i <= patternData.segmentsData.Length; i++)
            {
                segments[i - 1] = Instantiate(segmentPrefab, position, Quaternion.AngleAxis(angle * i, Vector3.up), transform);
                segments[i - 1].Initialize(patternData.segmentsData[i - 1], _tube, this);
            }

            platformDestroyed += _player.PlayIdleAnim;
            platformDestroyed += _player.EnableFallingTrail;
            platformDestroyed += _player.DisableTrail;
        }

        public void IncreaseTouchCounter()
        {
            countTouches++;
            if (countTouches == 3) DestroyPlatform();
        }
        
        public void DestroyPlatform()
        {
            platformDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}