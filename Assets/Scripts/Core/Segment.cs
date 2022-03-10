using Data.Core.Segments;
using UnityEngine;

namespace Core
{
    public class Segment : MonoBehaviour
    {
        public SegmentData segmentData;

        private Tube tube;
        private Platform platform;
        
        public void Initialize(SegmentData _segmentData, Tube _tube, Platform _platform)
        {
            platform = _platform;
            segmentData = _segmentData;
            tube = _tube;
            
            ChangeMaterial();
        }

        public void ChangeMaterial()
        {
            if (segmentData.segmentType == SegmentType.Hole)
                transform.GetComponent<MeshRenderer>().enabled = false;
            else
                transform.GetComponent<MeshRenderer>().material = tube.visualController.GetSegmentMaterial(segmentData.segmentType);
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