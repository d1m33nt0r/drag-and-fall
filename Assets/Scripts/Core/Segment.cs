using Data.Core.Segments;
using Data.Core.Segments.Content;
using Data.Shop.TubeSkins;
using UnityEngine;

namespace Core
{
    public class Segment : MonoBehaviour
    {
        public SegmentData segmentData;
        [SerializeField] private Transform spawnPoint;
        private Tube tube;
        private Platform platform;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject crystalPrefab;
        
        public void Initialize(SegmentData _segmentData, Tube _tube, Platform _platform)
        {
            platform = _platform;
            segmentData = _segmentData;
            tube = _tube;
            SpawnContent();
            ChangeTheme();
        }

        public void ChangeTheme()
        {
            transform.GetComponent<MeshRenderer>().enabled = true;
            
            if (segmentData.segmentType == SegmentType.Hole)
            {
                transform.GetComponent<MeshRenderer>().enabled = false;
            }
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

        public void SpawnContent()
        {
            switch (segmentData.segmentContent)
            {
                case SegmentContent.Coin:
                    Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
                    break;
                case SegmentContent.Crystal:
                    Instantiate(crystalPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
                    break;
            }
        }
        
        public void IncreasePlatformTouchCounter(ScorePanel scorePanel)
        {
            platform.IncreaseTouchCounter(scorePanel);
        }

        public void DestroyPlatform(ScorePanel scorePanel)
        {
            platform.DestroyPlatform(scorePanel);
        }
    }
}