using Core.Bonuses;
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
        private BonusController bonusController;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject crystalPrefab;
        [SerializeField] private GameObject x2;
        [SerializeField] private GameObject shield;
        [SerializeField] private GameObject nitro;
        [SerializeField] private GameObject magnet;
        [SerializeField] private GameObject key;
        
        public void Initialize(SegmentData _segmentData, Tube _tube, Platform _platform, BonusController _bonusController)
        {
            platform = _platform;
            segmentData = _segmentData;
            tube = _tube;
            bonusController = _bonusController;
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
                
                transform.GetComponent<MeshFilter>().mesh = _environmentSkinData.segment;
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
                case SegmentContent.Acceleration:
                    var accelerationInstance = Instantiate(nitro, spawnPoint.position, Quaternion.identity, spawnPoint);
                    accelerationInstance.GetComponent<Acceleration>().Construct(bonusController);
                    break;
                case SegmentContent.Multiplier:
                    var multiplierInstance = Instantiate(x2, spawnPoint.position, Quaternion.identity, spawnPoint);
                    multiplierInstance.GetComponent<Multiplier>().Construct(bonusController);
                    break;
                case SegmentContent.Shield:
                    var shieldInstance = Instantiate(shield, spawnPoint.position, Quaternion.identity, spawnPoint);
                    shieldInstance.GetComponent<Shield>().Construct(bonusController);
                    break;
                case SegmentContent.Magnet:
                    var magnetInstance = Instantiate(magnet, spawnPoint.position, Quaternion.identity, spawnPoint);
                    magnetInstance.GetComponent<Magnet>().Construct(bonusController);
                    break;
                case SegmentContent.Key:
                    var keyInstance = Instantiate(key, spawnPoint.position, Quaternion.identity, spawnPoint);
                    keyInstance.GetComponent<Key>().Construct(bonusController);
                    break;
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