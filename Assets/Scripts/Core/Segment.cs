using Data.Core.Segments;
using UnityEngine;

namespace Core
{
    public class Segment : MonoBehaviour
    {
        public SegmentData segmentData;

        private Platform platform;
        
        public void Initialize(SegmentData _segmentData, Tube _tube, Platform _platform)
        {
            platform = _platform;
            segmentData = _segmentData;
            
            if (_segmentData.segmentType == SegmentType.Hole)
            {
                transform.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                transform.GetComponent<MeshRenderer>().material = _tube.visualController.GetSegmentMaterial(_segmentData.segmentType);
            }
            
        }

        public void IncreasePlatformTouchCounter()
        {
            platform.IncreaseTouchCounter();
        }

        public void DestroyPlatform()
        {
            platform.DestroyPlatform();
        }
    }
}