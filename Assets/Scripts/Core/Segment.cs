using PatternManager;
using UnityEngine;

namespace Core
{
    public class Segment : MonoBehaviour
    {
        public SegmentData segmentData;

        public void Initialize(SegmentData _segmentData, Tube _tube)
        {
            segmentData = _segmentData;
            transform.GetComponent<MeshRenderer>().material = _tube.visualController.GetSegmentMaterial(_segmentData.segmentType);
        }
    }
}