using System;
using System.Collections;
using Common;
using Core.Bonuses;
using Data.Core;
using Data.Core.Segments;
using Data.Core.Segments.Content;
using Data.Shop.TubeSkins;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ObjectPool;
using Sound;
using UI;
using UnityEngine;

namespace Core
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] private TutorialUI tutorialUI;
        [SerializeField] private PlatformSound platformSound;
        [SerializeField] private EffectsPool effectsPool;
        [SerializeField] private PlatformPool platformPool;
        private bool isFirstPlatform = true;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private SegmentContentPool segmentContentPool;
        [SerializeField] private GainScore gainScore;
        [SerializeField] private Player player;
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private TubeMover tubeMover;
        [SerializeField] private int countPlatforms;
        
        
        [SerializeField] private float distanceBetweenPlatforms;
        [SerializeField] public float platformMovementSpeed;
        [SerializeField] private DragController dragController;
        [SerializeField] public VisualController visualController;
        public GameManager gameManager;
        public Concentration concentration;
        public LevelsData levelsData;
        [SerializeField] private BonusController bonusController;
        [SerializeField] private LevelProgress levelProgress;


        private TweenerCore<Quaternion, Vector3, QuaternionOptions> rotationTweenerCore;
        
        public bool platformsIsInitialized;
        public bool isLevelMode;
        private Vector3 startEulerAngles;
        private float startTime;
        private float journeyLength;
        
        private PatternData currentPatternData;
        public Platform[] platforms;
        
        private Vector3[] localPositionsOfPlatforms;
       
        private bool destroyTubeNeeded = true;
        private Coroutine movingCoroutine;
       
        
        private void Awake()
        {
            DragController.SwipeEvent += RotateTube;
            Initialize();
            startEulerAngles = transform.rotation.eulerAngles;
            journeyLength = Vector3.Distance(localPositionsOfPlatforms[0], localPositionsOfPlatforms[1]);
        }

        public void SetMovementSpeed(float speed)
        {
            platformMovementSpeed = speed;
            cameraController.ChangeFieldView(speed);
            
            if (platformMovementSpeed == 6)
            {
                player.SetActiveFireEffect(true);
                player.IncreaseFireEffect6();
            }
            else if (platformMovementSpeed == 5)
            {
                player.SetActiveFireEffect(true);
                player.IncreaseFireEffect5();
            }
            else if (platformMovementSpeed == 4)
            {
                player.SetActiveFireEffect(true);
                player.IncreaseFireEffect4();
            }
            else if (platformMovementSpeed == 3)
            {
                player.SetActiveFireEffect(false);
            }
        }
        
        public void FinishLevel(LevelData _levelData)
        {
            SetMovementSpeed(3f);
            gameManager.FinishLevel(_levelData);
        }
        
        public void Failed()
        {
            SetMovementSpeed(3f);
            if (isLevelMode) gameManager.FailedLevel();
            else gameManager.FailedGame();
        }

        public void IncreaseSpeed(float value)
        {
            SetMovementSpeed(platformMovementSpeed += value);
        }

        public void ResetDefaultSpeed()
        {
            SetMovementSpeed(3f);
            player.SetActiveFireEffect(false);
        }
        
        private void Initialize()
        {
            InitializePlatformPoints();
            InitializePlatforms();
            ChangeTheme();
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            tubeMover.TryOnSkin(_environmentSkinData);
            foreach (var platform in platforms)
            {
                for (var i = 0; i < Constants.Platform.COUNT_SEGMENTS; i++)
                    platform.transform.GetChild(i).GetComponent<Segment>().TryOnTheme(_environmentSkinData);
            }
        }
        
        public void ChangeTheme()
        {
            tubeMover.ChangeTheme();
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
                    //LeanTween.rotateY(gameObject, transform.rotation.eulerAngles.y - delta, 0.1f);
                    //transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y - delta / 2, eulerAngles.z);
                    transform.DORotate(new Vector3(eulerAngles.x, eulerAngles.y - delta, eulerAngles.z), 0.1f);
                    break;
                case DragController.SwipeType.RIGHT:
                    //LeanTween.rotateY(gameObject, transform.rotation.eulerAngles.y + delta, 0.1f);
                    //transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + delta / 2, eulerAngles.z);
                    transform.DORotate(new Vector3(eulerAngles.x, eulerAngles.y + delta, eulerAngles.z), 0.1f);
                    break;
            }
        }
        
        public void MovePlatforms()
        {
            startTime = Time.time;
            if (movingCoroutine != null) StopCoroutine(movingCoroutine);
            movingCoroutine = StartCoroutine(Moving(platformMovementSpeed));
        }

        private IEnumerator Moving(float speed)
        {
            destroyTubeNeeded = !destroyTubeNeeded;
            for (var i = 1; i < countPlatforms; i++)
                platforms[i - 1] = platforms[i];

            if (tutorialUI.firstGeneralStepComplete)
            {
                if (!isLevelMode)
                    currentPatternData = gameManager.gameMode.infinityMode.GetPatternData();
                else
                    currentPatternData = gameManager.gameMode.levelMode.GetPatternData();
            
                if (isLevelMode && currentPatternData != null)
                    CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
                else if (isLevelMode && currentPatternData == null)
                {
                    currentPatternData = new PatternData(12);
                    CreateNewPlatform(countPlatforms - 1, currentPatternData, true);
                }
                else
                    CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
            
                var distCovered = (Time.time - startTime) * speed;
                var fractionOfJourney = distCovered / journeyLength;
                while (distCovered / journeyLength != 1)
                {
                    if (!gameManager.gameStarted)
                    {
                        yield return null;
                        continue;
                    }
                
                    for (var i = 1; i < countPlatforms; i++)
                    {
                        try
                        {
                            distCovered = (Time.time - startTime) * speed;
                            fractionOfJourney = distCovered / journeyLength;
                            platforms[i - 1].transform.position = Vector3.Lerp(localPositionsOfPlatforms[i], localPositionsOfPlatforms[i - 1], fractionOfJourney);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }

                    yield return null;
                }
            }
            else
            {
                if (!concentration.isActive)
                {
                    GenerateStartTutorialPlatform();
                    CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
                }
                else
                {
                    isLevelMode = true;
                    gameManager.gameMode.levelMode.level = levelsData.leves[0];
                    currentPatternData = gameManager.gameMode.levelMode.GetPatternData();
                    if (isLevelMode && currentPatternData != null)
                        CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
                    else if(isLevelMode && currentPatternData == null)
                        CreateNewPlatform(countPlatforms - 1, new PatternData(12), true);
                }
            
                var distCovered = (Time.time - startTime) * speed;
                var fractionOfJourney = distCovered / journeyLength;
                while (distCovered / journeyLength != 1)
                {
                    if (!gameManager.gameStarted)
                    {
                        yield return null;
                        continue;
                    }
                
                    for (var i = 1; i < countPlatforms; i++)
                    {
                        try
                        {
                            distCovered = (Time.time - startTime) * speed;
                            fractionOfJourney = distCovered / journeyLength;
                            platforms[i - 1].transform.position = Vector3.Lerp(localPositionsOfPlatforms[i], localPositionsOfPlatforms[i - 1], fractionOfJourney);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }

                    yield return null;
                }
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
        
        public void InitializePlatforms()
        {
            if (platformsIsInitialized)
            {
                gameManager.gameMode.infinityMode.ResetPointers();
                gameManager.gameMode.levelMode.ResetPointer();
                for (var i = 0; i < countPlatforms; i++)
                {
                    platforms[i].DestroyPlatform(false);
                }

                isFirstPlatform = true;
            }
            
            platforms = new Platform[countPlatforms];

            if (tutorialUI.firstStepComplete) 
                StandardInit();
            else
                TutorialInit();
            
        }

        private void TutorialInit()
        {
            for (var i = 0; i < countPlatforms; i++)
            {
                if (i == 0)
                {
                    currentPatternData = new PatternData(12);
                }
                else
                {
                    GenerateStartTutorialPlatform();
                }
                    
                CreateNewPlatform(i, currentPatternData, false);
            }

            platformsIsInitialized = true;
        }

        private void GenerateStartTutorialPlatform()
        {
            currentPatternData = new PatternData(12);
            currentPatternData.segmentsData = new[]
            {
                new SegmentData {positionIndex = 0, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 1, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 2, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 3, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 4, segmentContent = SegmentContent.None, segmentType = SegmentType.Hole},
                new SegmentData {positionIndex = 5, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 6, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 7, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 8, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 9, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 10, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                new SegmentData {positionIndex = 11, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
            };
        }

        private void StandardInit()
        {
            for (var i = 0; i < countPlatforms; i++)
            {
                if (!isLevelMode)
                {
                    if (isFirstPlatform)
                    {
                        currentPatternData = new PatternData(12);
                        isFirstPlatform = false;
                    }
                    else
                    {
                        currentPatternData = gameManager.gameMode.infinityMode.GetPatternData();
                    }
                }
                else
                {
                    currentPatternData = gameManager.gameMode.levelMode.GetPatternData();
                }

                CreateNewPlatform(i, currentPatternData, false);
            }

            platformsIsInitialized = true;
        }

        private void CreateNewPlatform(int _platformIndex, PatternData patternData, bool hide)
        {
            var platform = platformPool.GetPlatform();
            platform.transform.position = localPositionsOfPlatforms[_platformIndex];
            platform.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            platform.transform.SetParent(transform);
                //Instantiate(platformPrefab, localPositionsOfPlatforms[_platformIndex], Quaternion.Euler(transform.rotation.eulerAngles), transform);
            platform.Initialize(patternData);
            
            //AlignRotation(platformInstance);
            platforms[_platformIndex] = platform;
            
            if (hide) platforms[_platformIndex].gameObject.SetActive(false);
        }

        public void LevelStep()
        {
            levelProgress.Step();
        }
        
        public void IncreaseConcentration()
        {
            concentration.IncreaseConcentration();   
        }

        public void ResetConcentration()
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

        public void DestroyPlatform(bool platformsIsMoving)
        {
            platforms[0].DestroyPlatform(platformsIsMoving);
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
