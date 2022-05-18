using System.Collections.Generic;
using Common;
using Core.Bonuses;
using Data.Core;
using Data.Core.Segments;
using Data.Core.Segments.Content;
using ObjectPool;
using Progress;
using Sound;
using UI;
using UnityEngine;
using Zenject;

namespace Core
{
    public class PlatformMover : MonoBehaviour
    {
        private float speed;
        private float fractionOfJourney;
        private float distCovered;
        private bool tutorialConcentrationComplete;
        private int tutorialPlatformCounter;
        private bool isMoving;
        public ProgressController progressController;
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


        [Inject] private Queue<GamePatternData> patternDataPool;
        
        public bool platformsIsInitialized;
        public bool isLevelMode;
        private Vector3 startEulerAngles;
        private float startTime;
        private float journeyLength;
        
        private GamePatternData currentPatternData;
        public Platform[] platforms;
        
        private Vector3[] localPositionsOfPlatforms;
       
        private bool destroyTubeNeeded = true;


        private void Start()
        {
            dragController.SwipeEvent += RotateTube;
            Initialize();
            startEulerAngles = transform.rotation.eulerAngles;
            journeyLength = Vector3.Distance(localPositionsOfPlatforms[0], localPositionsOfPlatforms[1]);
        }

        public void SetMovementSpeed(float speed)
        {
            platformMovementSpeed = speed;
            cameraController.ChangeFieldView(speed);
            
            
            if (platformMovementSpeed == 3)
            {
                player.SetActiveFireEffect(false);
            }
            else
            {
                player.SetActiveFireEffect(true);
                player.IncreaseFireEffect6();
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
            
            var themeIdentifier = progressController.currentState.environmentSkin.index.ToString() 
                                              + progressController.currentState.playerSkin.index;
            
            ChangeTheme(themeIdentifier, progressController.currentState.environmentSkin.index);
        }

        public void ChangeTheme(string themeIdentifier, int environmentThemeIndex)
        {
            tubeMover.ChangeTheme(themeIdentifier, environmentThemeIndex);
            foreach (var platform in platforms)
            {
                for (var i = 0; i < Constants.Platform.COUNT_SEGMENTS; i++)
                    platform.transform.GetChild(i).GetComponent<Segment>().ChangeTheme(themeIdentifier);
            }
        }

        public void EnableLevelMode(LevelData _levelData)
        {
            gameManager.gameMode.levelMode.SetLevelData(_levelData);
            levelProgress.Initialize(_levelData);
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
                    transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y - delta * 40 * Time.deltaTime, eulerAngles.z);
                    break;
                case DragController.SwipeType.RIGHT:
                    transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + delta * 40 * Time.deltaTime, eulerAngles.z);
                    break;
            }
        }

        public void MovePlatforms()
        {
            speed = platformMovementSpeed;
            startTime = Time.time;
            isMoving = true;

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
                    CreateNewPlatform(countPlatforms - 1, currentPatternData, true);
                }
                else
                    CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
            }
            else
            {
                if (!concentration.isActive && !tutorialConcentrationComplete)
                {
                    currentPatternData = GenerateStartTutorialPlatform();
                    CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
                }
                else
                {
                    tutorialConcentrationComplete = true;
                    isLevelMode = true;
                    gameManager.gameMode.levelMode.level = levelsData.leves[0];
                    currentPatternData = gameManager.gameMode.levelMode.GetPatternData();
                    if (isLevelMode && currentPatternData != null)
                        CreateNewPlatform(countPlatforms - 1, currentPatternData, false);
                    else if (isLevelMode && currentPatternData == null)
                        CreateNewPlatform(countPlatforms - 1, currentPatternData, true);
                }
            }
        }

        private void Update()
        {
            if (!gameManager.gameStarted) return;
            if (!isMoving) return;
            
            for (var i = 1; i < countPlatforms; i++)
            {
                distCovered = (Time.time - startTime) * speed;
                fractionOfJourney = distCovered / journeyLength;
                platforms[i - 1].transform.position = Vector3.Lerp(localPositionsOfPlatforms[i], localPositionsOfPlatforms[i - 1], fractionOfJourney);
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
                    currentPatternData = patternDataPool.Dequeue();
                    currentPatternData.Configure(new PatternData(12));
                }
                else
                {
                    currentPatternData = GenerateStartTutorialPlatform();
                }
                    
                CreateNewPlatform(i, currentPatternData, false);
            }

            platformsIsInitialized = true;
        }

        private GamePatternData GenerateStartTutorialPlatform()
        {
            var patternData = GetTutorialPatternData();
            var gamePatternData = patternDataPool.Dequeue();
            gamePatternData.Configure(patternData);
            return gamePatternData;
        }

        private PatternData GetTutorialPatternData()
        {
            var patternData = new PatternData(12);
            
            if (tutorialPlatformCounter == 0)
            {
                patternData.segmentsData = new[]
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
                    new SegmentData {positionIndex = 11, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground}
                };
                tutorialPlatformCounter++;
            }
            else if (tutorialPlatformCounter == 1)
            {
                patternData.segmentsData = new[]
                {
                    new SegmentData {positionIndex = 0, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 1, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 2, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 3, segmentContent = SegmentContent.None, segmentType = SegmentType.Hole},
                    new SegmentData {positionIndex = 4, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 5, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 6, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 7, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 8, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 9, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 10, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 11, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground}
                };
                tutorialPlatformCounter++;
            }
            else if (tutorialPlatformCounter == 2)
            {
                patternData.segmentsData = new[]
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
                    new SegmentData {positionIndex = 11, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground}
                };
                tutorialPlatformCounter++;
            }
            else if (tutorialPlatformCounter == 3)
            {
                patternData.segmentsData = new[]
                {
                    new SegmentData {positionIndex = 0, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 1, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 2, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 3, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 4, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 5, segmentContent = SegmentContent.None, segmentType = SegmentType.Hole},
                    new SegmentData {positionIndex = 6, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 7, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 8, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 9, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 10, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground},
                    new SegmentData {positionIndex = 11, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground}
                };
                tutorialPlatformCounter++;
            }
            else if (tutorialPlatformCounter == 4)
            {
                patternData.segmentsData = new[]
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
                    new SegmentData {positionIndex = 11, segmentContent = SegmentContent.None, segmentType = SegmentType.Ground}
                };
                tutorialPlatformCounter = 0;
            }
            
            return patternData;
        }

        private void StandardInit()
        {
            for (var i = 0; i < countPlatforms; i++)
            {
                if (!isLevelMode)
                {
                    if (isFirstPlatform)
                    {
                        currentPatternData = gameManager.gameMode.infinityMode.GetFirstPlatform();
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

        private void CreateNewPlatform(int platformIndex, GamePatternData patternData, bool hide)
        {
            var platform = platformPool.GetPlatform();
            var pTransform = platform.transform;
            pTransform.position = localPositionsOfPlatforms[platformIndex];
            pTransform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            pTransform.SetParent(transform);
                
            if (patternData != null)
            {
                platform.Initialize(patternData);
            }
    
            platforms[platformIndex] = platform;
            if (hide) platforms[platformIndex].gameObject.SetActive(false);
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
