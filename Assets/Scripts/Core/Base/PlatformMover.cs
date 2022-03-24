using System;
using System.Collections;
using Common;
using Core.Bonuses;
using Data.Core;
using Data.Shop.TubeSkins;
using DG.Tweening;
using UI;
using UnityEngine;

namespace Core
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] private GainScore gainScore;
        [SerializeField] private Player player;
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private GameObject tubePrefab;
        [SerializeField] private int countPlatforms;
        [SerializeField] private int countTubeParts;
        [SerializeField] private float distanceBetweenTubeParts;
        [SerializeField] private float distanceBetweenPlatforms;
        [SerializeField] public float platformMovementSpeed;
        [SerializeField] private DragController dragController;
        [SerializeField] public VisualController visualController;
        public GameManager gameManager;
        [SerializeField] private Concentration concentration;
        public LevelsData levelsData;
        [SerializeField] private BonusController bonusController;
        [SerializeField] private LevelProgress levelProgress;

        public bool tubeIsInitialized;
        public bool platformsIsInitialized;
        public bool isLevelMode;
        private Vector3 startEulerAngles;
        private float startTime;
        private float journeyLength;
        
        private PatternData currentPatternData;
        public Platform[] platforms;
        public TubePart[] tubeParts;
        private Vector3[] localPositionsOfPlatforms;
        private Vector3[] localPositionsOfTubeParts;
        private bool destroyTubeNeeded = true;
        
        private void Awake()
        {
            dragController.SwipeEvent += RotateTube;
            Initialize();
            startEulerAngles = transform.rotation.eulerAngles;
            journeyLength = Vector3.Distance(localPositionsOfPlatforms[0], localPositionsOfPlatforms[1]);
        }

        public void SetMovementSpeed(float speed)
        {
            platformMovementSpeed = speed;
        }
        
        public void FinishLevel(LevelData _levelData)
        {
            gameManager.FinishLevel(_levelData);
        }
        
        public void Failed()
        {
            if (isLevelMode) gameManager.FailedLevel();
            else gameManager.FailedGame();
        }

        public void IncreaseSpeed(float value)
        {
            platformMovementSpeed += value;
        }

        public void ResetDefaultSpeed()
        {
            platformMovementSpeed = 2.25f;
        }
        
        private void Initialize()
        {
            InitializeTubePoints();
            InitializePlatformPoints();
            InitializePlatforms();
            InitializeTube();
            ChangeTheme();
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            foreach (var tubePart in tubeParts)
            {
                tubePart.TryOnSkin(_environmentSkinData);
            }
            
            foreach (var platform in platforms)
            {
                for (var i = 0; i < Constants.Platform.COUNT_SEGMENTS; i++)
                    platform.transform.GetChild(i).GetComponent<Segment>().TryOnTheme(_environmentSkinData);
            }
        }
        
        public void ChangeTheme()
        {
            foreach (var tubePart in tubeParts)
            {
                tubePart.ChangeTheme();
            }
            
            foreach (var platform in platforms)
            {
                for (var i = 0; i < Constants.Platform.COUNT_SEGMENTS; i++)
                    platform.transform.GetChild(i).GetComponent<Segment>().ChangeTheme();
            }
        }

        public void EnableLevelMode(LevelData _levelData)
        {
            gameManager.gameMode.levelMode.SetLevelData(_levelData);
            levelProgress.Initialize(_levelData);
            InitializePlatforms();
        }

        public void SetLevelMode(bool value)
        {
            isLevelMode = value;
            InitializePlatforms();
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
            destroyTubeNeeded = !destroyTubeNeeded;
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

            var targetPositions = new Vector3[countTubeParts];
            var currentPositions = new Vector3[countTubeParts];

            for (var i = 0; i < countTubeParts; i++)
            {
                currentPositions[i] = tubeParts[i].transform.position;
                targetPositions[i] = currentPositions[i] + Vector3.up;
            }

            float distCovered = (Time.time - startTime) * platformMovementSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            
            while (distCovered / journeyLength != 1)
            {
                for (var i = 1; i < countPlatforms; i++)
                {
                    distCovered = (Time.time - startTime) * platformMovementSpeed;
                    fractionOfJourney = distCovered / journeyLength;
                    platforms[i - 1].transform.position = Vector3.Lerp(localPositionsOfPlatforms[i], localPositionsOfPlatforms[i - 1], fractionOfJourney);
                }

                yield return null;
            }
        }

        public IEnumerator MovingTube()
        {
            float distCovered = (Time.time - startTime) * platformMovementSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            while (distCovered / journeyLength != 1)
            {
                for (var i = 0; i < countTubeParts; i++)
                {
                    distCovered = (Time.time - startTime) * platformMovementSpeed;
                    fractionOfJourney = distCovered / journeyLength;
                    //tubeParts[i].transform.position = Vector3.Lerp(currentPositions[i], targetPositions[i], fractionOfJourney);
                }

                yield return null;
            }
            
        }

        public void CreateNewTubePart()
        {
            for (var i = 1; i < countTubeParts; i++)
                tubeParts[i - 1] = tubeParts[i];
            
            var tubePartInstance = Instantiate(tubePrefab, 
                tubeParts[tubeParts.Length - 2].transform.position - Vector3.down * 2, 
                Quaternion.Euler(transform.rotation.eulerAngles), transform);
                
            tubePartInstance.GetComponent<TubePart>().Initialize(this);
            tubeParts[tubeParts.Length - 1] = tubePartInstance.GetComponent<TubePart>();
            AlignRotation(tubePartInstance);
        }

        private void InitializePlatformPoints()
        {
            localPositionsOfPlatforms = new Vector3[countPlatforms];
        
            for (var i = 0; i < countPlatforms; i++)
            {
                localPositionsOfPlatforms[i] = new Vector3(transform.position.x, transform.localPosition.y - distanceBetweenPlatforms * i, transform.position.z);
            }
        }
        
        private void InitializeTubePoints()
        {
            localPositionsOfTubeParts = new Vector3[countTubeParts];
            
            var j = 4;
            for (var i = 0; i < countTubeParts; i++)
            {
                localPositionsOfTubeParts[i] = new Vector3(transform.position.x, transform.localPosition.y + j, transform.position.z);
                j -= 2;
            }
        }
        
        public void InitializePlatforms()
        {
            if (platformsIsInitialized)
            {
                gameManager.gameMode.infinityMode.ResetPointers();
                gameManager.gameMode.levelMode.ResetPointer();
                for (var i = 0; i < countPlatforms; i++)
                {
                    Destroy(platforms[i].gameObject);
                }
            }
            
            platforms = new Platform[countPlatforms];

            for (var i = 0; i < countPlatforms; i++)
            {
                if (!isLevelMode)
                    currentPatternData = gameManager.gameMode.infinityMode.GetPatternData();
                else
                    currentPatternData = gameManager.gameMode.levelMode.GetPatternData();
                
                CreateNewPlatform(i, currentPatternData, false);
            }

            platformsIsInitialized = true;
        }

        public void InitializeTube()
        {
            if (tubeIsInitialized)
            {
                for (var i = 0; i < countTubeParts; i++)
                {
                    Destroy(tubeParts[i].gameObject);
                }
            }
            
            tubeParts = new TubePart[countTubeParts];

            for (var i = 0; i < countTubeParts; i++)
            {
                var tubePartInstance = Instantiate(tubePrefab, localPositionsOfTubeParts[i], Quaternion.Euler(transform.rotation.eulerAngles),
                    transform);
                tubePartInstance.GetComponent<TubePart>().Initialize(this);
                tubeParts[i] = tubePartInstance.GetComponent<TubePart>();
                AlignRotation(tubePartInstance);
            }

            tubeIsInitialized = true;
        }

        private void CreateNewPlatform(int _platformIndex, PatternData patternData, bool hide)
        {
            var platformInstance = Instantiate(platformPrefab, localPositionsOfPlatforms[_platformIndex], Quaternion.Euler(transform.rotation.eulerAngles), transform);
            var platform = platformInstance.GetComponent<Platform>();
            platform.Initialize(Constants.Platform.COUNT_SEGMENTS, patternData, this, player, bonusController, gainScore);
            platform.increaseConcentraion += IncreaseConcentration;
            platform.resetConcentraion += ResetConcentration;
            platform.increaseConcentraion += LevelStep;
            
            AlignRotation(platformInstance);
            platforms[_platformIndex] = platform;
            
            if (hide) platforms[_platformIndex].gameObject.SetActive(false);
        }

        private void LevelStep()
        {
            levelProgress.Step();
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
            player.SetFallingTrailState();
            
            for (var i = 0; i < platforms.Length; i++)
                platforms[i].gameObject.SetActive(false);
        }

        public void DestroyPlatform()
        {
            platforms[0].DestroyPlatform();
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
