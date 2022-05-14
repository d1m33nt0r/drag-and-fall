using Core.Bonuses;
using Core.Effects;
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
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        public MeshCollider MeshCollider { get; private set; }
        private void Awake()
        {
            MeshCollider = GetComponent<MeshCollider>();
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Construct(PlatformMover platformMover, Platform platform, 
            BonusController bonusController, SegmentContentPool segmentContentPool)
        {
            this.segmentContentPool = segmentContentPool;
            this.platform = platform;
            this.platformMover = platformMover;
            this.bonusController = bonusController;
        }
        
        public void Initialize(SegmentData _segmentData)
        {
            segmentData = _segmentData;
            SpawnContent();
            ChangeTheme();
        }

        public void ChangeTheme()
        {
            meshRenderer.enabled = true;
            
            if (segmentData.segmentType == SegmentType.Hole)
            {
                meshRenderer.enabled = false;
            }
            else
            {
                meshRenderer.material = platformMover.visualController.GetSegmentMaterial(segmentData.segmentType);
                meshFilter.mesh = platformMover.visualController.GetSegmentMesh(segmentData.segmentType);
            }
        }

        public void ChangeColor(int countTouches)
        {
            if (segmentData.segmentType == SegmentType.Hole) return;
            if (segmentData.segmentType == SegmentType.Let && countTouches <= 2) return;
           
            meshFilter.mesh =
                platformMover.visualController.GetPlatformColors()[countTouches - 1];
        }
        
        public void TryOnTheme(EnvironmentSkinData _environmentSkinData)
        {
            if (segmentData.segmentType == SegmentType.Hole)
                meshRenderer.enabled = false;
            else
            {
                if (segmentData.segmentType == SegmentType.Ground)
                {
                    meshRenderer.material = platformMover.visualController.TryOnSegmentMaterial(_environmentSkinData);
                }
                else
                {
                    meshRenderer.material = platformMover.visualController.TryOnSegmentMaterial(_environmentSkinData);
                }
                
                meshFilter.mesh = platformMover.visualController.GetSegmentMesh(segmentData.segmentType);
            }
        }

        public void SpawnContent()
        {
            switch (segmentData.segmentContent)
            {
                case SegmentContent.Coin:
                    var coinInstance = segmentContentPool.GetObject(SegmentContent.Coin);
                    var coTransform = coinInstance.transform;
                    coTransform.position = spawnPoint.position;
                    coTransform.rotation = Quaternion.identity;
                    coTransform.SetParent(spawnPoint);
                    coinInstance.GetComponent<Coin>().Construct(segmentContentPool);
                    break;
                case SegmentContent.Crystal:
                    var crystalInstance = segmentContentPool.GetObject(SegmentContent.Crystal);
                    var crTransform = crystalInstance.transform;
                    crTransform.position = spawnPoint.position;
                    crTransform.rotation = Quaternion.identity;
                    crTransform.SetParent(spawnPoint);
                    crystalInstance.GetComponent<Crystal>().Construct(segmentContentPool);
                    break;
                case SegmentContent.Acceleration:
                    var accelerationInstance = segmentContentPool.GetObject(SegmentContent.Acceleration);
                    var acTransform = accelerationInstance.transform;
                    acTransform.position = spawnPoint.position;
                    acTransform.rotation = Quaternion.identity;
                    acTransform.SetParent(spawnPoint);
                    accelerationInstance.GetComponent<Acceleration>().Construct(bonusController, segmentContentPool);
                    break;
                case SegmentContent.Multiplier:
                    var multiplierInstance = segmentContentPool.GetObject(SegmentContent.Multiplier);
                    var muTransform = multiplierInstance.transform;
                    muTransform.position = spawnPoint.position;
                    muTransform.rotation = Quaternion.identity;
                    muTransform.SetParent(spawnPoint);
                    multiplierInstance.GetComponent<Multiplier>().Construct(bonusController, segmentContentPool);
                    break;
                case SegmentContent.Shield:
                    var shieldInstance = segmentContentPool.GetObject(SegmentContent.Shield);
                    var shTransform = shieldInstance.transform;
                    shTransform.position = spawnPoint.position;
                    shTransform.rotation = Quaternion.identity;
                    shTransform.SetParent(spawnPoint);
                    shieldInstance.GetComponent<Shield>().Construct(bonusController, segmentContentPool);
                    break;
                case SegmentContent.Magnet:
                    var magnetInstance = segmentContentPool.GetObject(SegmentContent.Magnet);
                    var maTransform = magnetInstance.transform;
                    maTransform.position = spawnPoint.position;
                    maTransform.rotation = Quaternion.identity;
                    maTransform.SetParent(spawnPoint);
                    magnetInstance.GetComponent<Magnet>().Construct(bonusController, segmentContentPool);
                    break;
                case SegmentContent.Key:
                    var keyInstance = segmentContentPool.GetObject(SegmentContent.Key);
                    var keTransform = keyInstance.transform;
                    keTransform.position = spawnPoint.position;
                    keTransform.rotation = Quaternion.identity;
                    keTransform.SetParent(spawnPoint);
                    keyInstance.GetComponent<Key>().Construct(bonusController, segmentContentPool);
                    break;
            }
        }

        public void ReturnTouchEffectToPool()
        {
            if (transform.childCount > 1)
                transform.GetChild(1).GetComponent<TouchEffect>().ReturnToPool();
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