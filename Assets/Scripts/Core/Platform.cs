using PatternManager;
using UnityEngine;

namespace Core
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Segment segmentPrefab;
        
        private Segment[] segments;
        private float angle;
        
        public void Initialize(int _segmentsCount, PatternData patternData, Tube _tube)
        {
            angle = 360 / _segmentsCount;
            segments = new Segment[_segmentsCount];
            var position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

            for (int i = 1; i <= patternData.segmentsData.Length; i++)
            {
                segments[i - 1] = Instantiate(segmentPrefab, position, Quaternion.AngleAxis(angle * i, Vector3.up), transform);
                segments[i - 1].Initialize(patternData.segmentsData[i - 1], _tube);
            }
        }
    }
}