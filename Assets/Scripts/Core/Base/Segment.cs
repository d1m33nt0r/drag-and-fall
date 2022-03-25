using Core.Bonuses;
using Data.Core.Segments;
using Data.Core.Segments.Content;
using Data.Shop.TubeSkins;
using ObjectPool;
using UnityEngine;

namespace Core
{
    public class Segment : MonoBehaviour
    {
        public SegmentData segmentData;
        [SerializeField] private Transform spawnPoint;
        private PlatformMover platformMover;
        private Platform platform;
        private BonusController bonusController;
        private SegmentContentPool segmentContentPool;

        public void Initialize(SegmentData _segmentData, PlatformMover platformMover, Platform _platform, 
            BonusController _bonusController, SegmentContentPool segmentContentPool)
        {
            this.segmentContentPool = segmentContentPool;
            platform = _platform;
            segmentData = _segmentData;
            this.platformMover = platformMover;
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
                transform.GetComponent<MeshRenderer>().material = platformMover.visualController.GetSegmentMaterial(segmentData.segmentType);
                transform.GetComponent<MeshFilter>().mesh = platformMover.visualController.GetSegmentMesh();
            }
        }

        public void ChangeColor(int countTouches)
        {
            if (segmentData.segmentType == SegmentType.Hole) return;
            if (segmentData.segmentType == SegmentType.Let && countTouches <= 2) return;
            transform.GetComponent<MeshRenderer>().material.color = platformMover.visualController.GetPlatformColors()[countTouches];
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
                    var coinInstance = segmentContentPool.GetObject(SegmentContent.Coin);
                    coinInstance.transform.position = spawnPoint.position;
                    coinInstance.transform.rotation = Quaternion.identity;
                    coinInstance.transform.SetParent(spawnPoint);
                    coinInstance.GetComponent<Coin>().Construct(segmentContentPool);
                    break;
                case SegmentContent.Crystal:
                    var crystalInstance = segmentContentPool.GetObject(SegmentContent.Crystal);
                    crystalInstance.transform.position = spawnPoint.position;
                    crystalInstance.transform.rotation = Quaternion.identity;
                    crystalInstance.transform.SetParent(spawnPoint);
                    crystalInstance.GetComponent<Crystal>().Construct(segmentContentPool);
                    break;
                case SegmentContent.Acceleration:
                    var accelerationInstance = segmentContentPool.GetObject(SegmentContent.Acceleration);
                    accelerationInstance.transform.position = spawnPoint.position;
                    accelerationInstance.transform.rotation = Quaternion.identity;
                    accelerationInstance.transform.SetParent(spawnPoint);
                    accelerationInstance.GetComponent<Acceleration>().Construct(bonusController, segmentContentPool);
                    break;
                case SegmentContent.Multiplier:
                    var multiplierInstance = segmentContentPool.GetObject(SegmentContent.Multiplier);
                    multiplierInstance.transform.position = spawnPoint.position;
                    multiplierInstance.transform.rotation = Quaternion.identity;
                    multiplierInstance.transform.SetParent(spawnPoint);
                    multiplierInstance.GetComponent<Multiplier>().Construct(bonusController, segmentContentPool);
                    break;
                case SegmentContent.Shield:
                    var shieldInstance = segmentContentPool.GetObject(SegmentContent.Shield);
                    shieldInstance.transform.position = spawnPoint.position;
                    shieldInstance.transform.rotation = Quaternion.identity;
                    shieldInstance.transform.SetParent(spawnPoint);
                    shieldInstance.GetComponent<Shield>().Construct(bonusController, segmentContentPool);
                    break;
                case SegmentContent.Magnet:
                    var magnetInstance = segmentContentPool.GetObject(SegmentContent.Magnet);
                    magnetInstance.transform.position = spawnPoint.position;
                    magnetInstance.transform.rotation = Quaternion.identity;
                    magnetInstance.transform.SetParent(spawnPoint);
                    magnetInstance.GetComponent<Magnet>().Construct(bonusController, segmentContentPool);
                    break;
                case SegmentContent.Key:
                    var keyInstance = segmentContentPool.GetObject(SegmentContent.Key);
                    keyInstance.transform.position = spawnPoint.position;
                    keyInstance.transform.rotation = Quaternion.identity;
                    keyInstance.transform.SetParent(spawnPoint);
                    keyInstance.GetComponent<Key>().Construct(bonusController, segmentContentPool);
                    break;
            }
        }

        public void ReturnSegmentContentToPool()
        {
            switch (segmentData.segmentContent)
            {
                case SegmentContent.Coin:
                    if (transform.GetChild(0).childCount > 0)
                        segmentContentPool.ReturnObjectToPool(SegmentContent.Coin, transform.GetChild(0).GetChild(0).gameObject);
                    break;
                case SegmentContent.Crystal:
                    if (transform.GetChild(0).childCount > 0)
                        segmentContentPool.ReturnObjectToPool(SegmentContent.Crystal, transform.GetChild(0).GetChild(0).gameObject);
                    break;
                case SegmentContent.Acceleration:
                    if (transform.GetChild(0).childCount > 0)
                        segmentContentPool.ReturnObjectToPool(SegmentContent.Acceleration, transform.GetChild(0).GetChild(0).gameObject);
                    break;
                case SegmentContent.Multiplier:
                    if (transform.GetChild(0).childCount > 0)
                        segmentContentPool.ReturnObjectToPool(SegmentContent.Multiplier, transform.GetChild(0).GetChild(0).gameObject);
                    break;
                case SegmentContent.Shield:
                    if (transform.GetChild(0).childCount > 0)
                        segmentContentPool.ReturnObjectToPool(SegmentContent.Shield, transform.GetChild(0).GetChild(0).gameObject);
                    break;
                case SegmentContent.Magnet:
                    if (transform.GetChild(0).childCount > 0)
                        segmentContentPool.ReturnObjectToPool(SegmentContent.Magnet, transform.GetChild(0).GetChild(0).gameObject);
                    break;
                case SegmentContent.Key:
                    if (transform.GetChild(0).childCount > 0)
                        segmentContentPool.ReturnObjectToPool(SegmentContent.Key, transform.GetChild(0).GetChild(0).gameObject);
                    break;
            }
        }
        
        public void IncreasePlatformTouchCounter()
        {
            platform.IncreaseTouchCounter();
        }
    }
}