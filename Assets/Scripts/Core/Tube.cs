using System.Collections;
using Common;
using Data.Core;
using Data.Core.Segments;
using Data.Shop.TubeSkins;
using DG.Tweening;
using UnityEngine;

namespace Core
{
    public class Tube : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private int countPlatforms;
        [SerializeField] private float distanceBetweenPlatforms;
        [SerializeField] private float platformMovementSpeed;
        [SerializeField] private DragController dragController;
        [SerializeField] public VisualController visualController;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Concentration concentration;
        
        private Vector3 startEulerAngles;
        private float startTime;
        private float journeyLength;
        
        private PatternData currentPatternData;
        public Platform[] platforms;
        private Vector3[] localPositionsOfPlatforms;
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        
        private void Awake()
        {
            meshFilter = transform.GetComponent<MeshFilter>();
            meshRenderer = transform.GetComponent<MeshRenderer>();
            dragController.SwipeEvent += RotateTube;
            Initialize();
            startEulerAngles = transform.rotation.eulerAngles;
            journeyLength = Vector3.Distance(platforms[0].transform.position, platforms[1].transform.position);
        }

        private void Initialize()
        {
            ChangeTheme();
            
            InitializePlatformPoints();
            InitializePlatforms();
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            meshRenderer.material = _environmentSkinData.tubeMaterial;
            meshFilter.mesh = _environmentSkinData.tube;
            
            foreach (var platform in platforms)
            {
                for (var i = 0; i < Constants.Platform.COUNT_SEGMENTS; i++)
                    platform.transform.GetChild(i).GetComponent<Segment>().TryOnTheme(_environmentSkinData);
            }
        }
        
        public void ChangeTheme()
        {
            meshRenderer.material = visualController.GetTubeMaterial();
            meshFilter.mesh = visualController.GetTubeMesh();
            
            foreach (var platform in platforms)
            {
                for (var i = 0; i < Constants.Platform.COUNT_SEGMENTS; i++)
                    platform.transform.GetChild(i).GetComponent<Segment>().ChangeTheme();
            }
        }
        
        private void RotateTube(DragController.SwipeType type, float delta)
        {
            var eulerAngles = transform.rotation.eulerAngles;
            switch (type)
            {
                case DragController.SwipeType.LEFT:
                    transform.DORotate(new Vector3(eulerAngles.x, eulerAngles.y - delta, eulerAngles.z), 0.1f);
                    break;
                case DragController.SwipeType.RIGHT:
                    transform.DORotate(new Vector3(eulerAngles.x, eulerAngles.y + delta, eulerAngles.z), 0.1f);
                    break;
            }
        }
        
        public void MovePlatforms()
        {
            startTime = Time.time;
            StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            for (var i = 1; i < countPlatforms; i++)
                platforms[i - 1] = platforms[i];
            
            CreateNewPlatform(countPlatforms - 1);
            
            while (platforms[0].transform.position != localPositionsOfPlatforms[0])
            {
                for (var i = 1; i < countPlatforms; i++)
                {
                    var distCovered = (Time.time - startTime) * platformMovementSpeed;
                    var fractionOfJourney = distCovered / journeyLength;
                    platforms[i - 1].transform.position = Vector3.Lerp(localPositionsOfPlatforms[i], localPositionsOfPlatforms[i - 1], fractionOfJourney);
                }

                yield return null;
            }
        }

        private void InitializePlatformPoints()
        {
            localPositionsOfPlatforms = new Vector3[countPlatforms];
        
            for (var i = 0; i < countPlatforms; i++)
            {
                localPositionsOfPlatforms[i] = new Vector3(transform.position.x, transform.localPosition.y - distanceBetweenPlatforms * i, transform.position.z);
            }
        }
    
        private void InitializePlatforms()
        {
            platforms = new Platform[countPlatforms];
        
            for (var i = 0; i < countPlatforms; i++)
                CreateNewPlatform(i);
        }

        private void CreateNewPlatform(int _platformIndex)
        {
            var platformInstance = Instantiate(platformPrefab, localPositionsOfPlatforms[_platformIndex], Quaternion.identity, transform);
            
            AlignRotation(platformInstance);
            
            var patternData = gameManager.gameMode.infinityMode.GetPatternData();
            
            var platform = platformInstance.GetComponent<Platform>();
            platform.Initialize(Constants.Platform.COUNT_SEGMENTS, patternData, this, player);
            platform.increaseConcentraion += IncreaseConcentration;
            platform.resetConcentraion += ResetConcentration;
            
            platforms[_platformIndex] = platform;
        }

        private void IncreaseConcentration()
        {
            concentration.IncreaseConcentration();   
        }

        private void ResetConcentration()
        {
            concentration.Reset();
        }
        
        private void AlignRotation(GameObject platformInstance)
        {
            if (transform.rotation.eulerAngles.y > 0)
            {
                platformInstance.transform.localRotation = Quaternion.Euler(startEulerAngles.x, 0 + 
                    Mathf.Abs(startEulerAngles.y - transform.rotation.eulerAngles.y), startEulerAngles.z);
            }
            else
            {
                platformInstance.transform.localRotation = Quaternion.Euler(startEulerAngles.x, 0 - 
                    Mathf.Abs(startEulerAngles.y - transform.rotation.eulerAngles.y), startEulerAngles.z);
            }
        }

        public void SetDefaultState()
        {
            player.SetDefaultState();
            
            for (var i = 0; i < platforms.Length; i++)
                platforms[i].gameObject.SetActive(true);
        }
        
        public void SetShopFallingTrail()
        {
            player.SetShopFallingTrailState();
            
            for (var i = 0; i < platforms.Length; i++)
                platforms[i].gameObject.SetActive(false);
        }

        public void SetShopState()
        {
            player.SetDefaultState();
            
            platforms[0].gameObject.SetActive(true);
            
            for (var i = 1; i < platforms.Length; i++)
                platforms[i].gameObject.SetActive(false);
        }
    }
}
