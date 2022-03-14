using System.Collections;
using Common;
using Data.Core;
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
        public GameManager gameManager;
        [SerializeField] private Concentration concentration;
        public LevelsData levelsData;
        
        public bool isLevelMode;
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
            player.failed += Failed;
            meshFilter = transform.GetComponent<MeshFilter>();
            meshRenderer = transform.GetComponent<MeshRenderer>();
            dragController.SwipeEvent += RotateTube;
            Initialize();
            startEulerAngles = transform.rotation.eulerAngles;
            journeyLength = Vector3.Distance(platforms[0].transform.position, platforms[1].transform.position);
        }

        public void FinishLevel(LevelData _levelData)
        {
            gameManager.FinishLevel(_levelData);
        }
        
        private void Failed()
        {
            if (isLevelMode)
                gameManager.FailedLevel();
            else
                gameManager.FailedGame();
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

        public void EnableLevelMode(LevelData _levelData)
        {
            gameManager.gameMode.levelMode.SetLevelData(_levelData);
            ReinitPlatforms();
        }

        public void SetLevelMode(bool value)
        {
            isLevelMode = value;
            ReinitPlatforms();
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
            
    
            if (!isLevelMode)
                currentPatternData = gameManager.gameMode.infinityMode.GetPatternData();
            else
                currentPatternData = gameManager.gameMode.levelMode.GetPatternData();
            
            if (isLevelMode && currentPatternData != null)
                CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
            else if(isLevelMode && currentPatternData == null)
                CreateNewPlatform(countPlatforms - 1, new PatternData(12), true);
            else
                CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
            
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

        public void ReinitPlatforms()
        {
            for (var i = 0; i < platforms.Length; i++)
            {
                if (!isLevelMode)
                    currentPatternData = gameManager.gameMode.infinityMode.GetPatternData();
                else
                    currentPatternData = gameManager.gameMode.levelMode.GetPatternData();
                
                platforms[i].ReInitialize(currentPatternData, this);
            }
        }
        
        public void InitializePlatforms()
        {
            platforms = new Platform[countPlatforms];

            for (var i = 0; i < countPlatforms; i++)
            {
                if (!isLevelMode)
                    currentPatternData = gameManager.gameMode.infinityMode.GetPatternData();
                else
                    currentPatternData = gameManager.gameMode.levelMode.GetPatternData();
                
                CreateNewPlatform(i, currentPatternData, false);
            }
        }

        private void CreateNewPlatform(int _platformIndex, PatternData patternData, bool hide)
        {
            var platformInstance = Instantiate(platformPrefab, localPositionsOfPlatforms[_platformIndex], Quaternion.identity, transform);
            
            AlignRotation(platformInstance);

            var platform = platformInstance.GetComponent<Platform>();
            platform.Initialize(Constants.Platform.COUNT_SEGMENTS, patternData, this, player);
            platform.increaseConcentraion += IncreaseConcentration;
            platform.resetConcentraion += ResetConcentration;
            
            platforms[_platformIndex] = platform;
            
            if (hide) platforms[_platformIndex].gameObject.SetActive(false);
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
