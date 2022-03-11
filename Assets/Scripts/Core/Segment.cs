using Data.Core.Segments;
using Data.Shop.TubeSkins;
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
            
            ChangeTheme();
        }

        public void ChangeTheme()
        {
            if (segmentData.segmentType == SegmentType.Hole)
                transform.GetComponent<MeshRenderer>().enabled = false;
            else
            {
                transform.GetComponent<MeshRenderer>().material = tube.visualController.GetSegmentMaterial(segmentData.segmentType);
                transform.GetComponent<MeshFilter>().mesh = tube.visualController.GetSegmentMesh();
            }
        }

        public void TryOnTheme(EnvironmentSkinData _environmentSkinData)
        {
            if (segmentData.segmentType == SegmentType.Hole)
                transform.GetComponent<MeshRenderer>().enabled = false;
            else
            {
                if (segmentData.segmentType == SegmentType.Ground)
                    transform.GetComponent<MeshRenderer>().material = _environmentSkinData.groundSegmentMaterial;
                else
                    transform.GetComponent<MeshRenderer>().material = _environmentSkinData.letSegmentMaterial;
                
                transform.GetComponent<MeshFilter>().mesh = tube.visualController.GetSegmentMesh();
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