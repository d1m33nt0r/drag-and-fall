using System;
using Core.Bonuses;
using Core.Effects;
using Cysharp.Threading.Tasks;
using Data;
using Data.Core.Segments;
using DG.Tweening;
using ObjectPool;
using Sound;
using UI.InfinityUI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class Player : MonoBehaviour
    {
        private const string IDLE_ANIMATION_IDENTIFIER = "Idle";
        private const string BOUNCE_ANIMATION_IDENTIFIER = "Bounce";
        private const string SEGMENT_TAG = "Segment";
        private const string BOUNCE_TRIGGER_TAG = "BounceTrigger";

        private Segment triggeredSegment;
        private TouchEffect touchEffect;
        private PlayerParticles playerParticles;
        private Platform platform;
        
        public event Action failed;
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private PlayerSounds playerSounds;
        [SerializeField] private PlatformSound platformSound;
        [SerializeField] private TubeMover tubeMover;
        [SerializeField] private EffectsPool effectsPool;
        [SerializeField] private GameObject shieldEffect;
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private MagnetPlayer magnetPlayer;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject fallingTrail;
        [SerializeField] private GameObject trail;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private VisualController visualController;
        [SerializeField] private BonusController bonusController;
        [SerializeField] private CoinPanel coinPanel;
        [SerializeField] private CrystalPanel crystalPanel;
        [SerializeField] private KeyPanel keyPanel;
        [SerializeField] private FailedInfinityUI failedInfinityUI;
        [SerializeField] private SessionData sessionData;
        [SerializeField] private FreeSpeedIncrease freeSpeedIncrease;
        [SerializeField] private GameObject fireEffect;
        [SerializeField] private GameObject sled;
        [SerializeField] private GameObject fireBacklight;
        
        private bool triggerStay;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        public void SetActiveFireEffect(bool value)
        {
            fireEffect.SetActive(value);
            fireBacklight.SetActive(value);
            if (!value) playerSounds.StopFireSound();
        }

        public void MaximumFire()
        {
            playerSounds.PlayFireSound();
            var fireParticle = fireEffect.GetComponent<ParticleSystem>();
            var main = fireParticle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 1, 1, 1f), new Color(1, 1, 1, 0.5f));
            main.startSpeed = -3;
            var backlightParticle = fireBacklight.GetComponent<ParticleSystem>();
            var backlightMain = backlightParticle.main;
            backlightMain.startColor = new ParticleSystem.MinMaxGradient(new Color(backlightMain.startColor.color.r,
                backlightMain.startColor.color.g, backlightMain.startColor.color.b, 0.25f));
        }
        
        public void RandomRotate()
        {
            var randomX = Random.Range(-35, 35);
            var randomY = Random.Range(-35, 60);
            var randomZ = Random.Range(0, 35);
            DoRotate(new Vector3(randomX, randomY, randomZ), 300);
            //transform.DORotate(new Vector3(randomX, randomY, randomZ), 0.5f);
        }

        private async UniTask DoRotate(Vector3 endValue, float speed)
        {
            var startTime = Time.time;
            var startValue = transform.rotation.eulerAngles;
            var journeyLength = Vector3.Distance(startValue, endValue);
            var fractionOfJourney = 0f;
            var distCovered = 0f;
            
            while (distCovered < journeyLength)
            { 
                distCovered = (Time.time - startTime) * speed;
                fractionOfJourney = distCovered / journeyLength;
                transform.rotation = Quaternion.Euler(Vector3.Lerp(startValue, endValue, fractionOfJourney));
                await UniTask.Yield();
            }
        }

        public void SpawnBonusCollectingEffect()
        {
            var effect = effectsPool.GetBonusCollectingEffect();
            effect.transform.SetParent(null);
            effect.transform.position = transform.position;
            effect.GetComponent<ParticleSystem>().Play();
        }
        
        public void SpawnCoinCollectingEffect()
        {
            var effect = effectsPool.GetCoinCollectingEffect();
            effect.transform.SetParent(null);
            effect.transform.position = transform.position;
            effect.GetComponent<ParticleSystem>().Play();
        }
        
        public void SpawnCrystalCollectingEffect()
        {
            var effect = effectsPool.GetCrystalCollectingEffect();
            effect.transform.SetParent(null);
            effect.transform.position = transform.position;
            effect.GetComponent<ParticleSystem>().Play();
        }
        
        public void SpawnKeyCollectingEffect()
        {
            var effect = effectsPool.GetKeyCollectingEffect();
            effect.transform.SetParent(null);
            effect.transform.position = transform.position;
            effect.GetComponent<ParticleSystem>().Play();
        }

        public void IncreaseFireEffect6()
        {
            playerSounds.PlayFireSound();
            var fireParticle = fireEffect.GetComponent<ParticleSystem>();
            var main = fireParticle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 1, 1, 1f), new Color(1, 1, 1, 0.5f));
            main.startSpeed = -3;
            var backlightParticle = fireBacklight.GetComponent<ParticleSystem>();
            var backlightMain = backlightParticle.main;
            backlightMain.startColor = new ParticleSystem.MinMaxGradient(new Color(backlightMain.startColor.color.r,
                backlightMain.startColor.color.g, backlightMain.startColor.color.b, 0.25f));

        }

        public void IncreaseFireEffect5()
        {
            var fireParticle = fireEffect.GetComponent<ParticleSystem>();
            var main = fireParticle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 1, 1, 0.25f), new Color(1, 1, 1, 0.2f));
            main.startSpeed = -2.5f;
            var backlightParticle = fireBacklight.GetComponent<ParticleSystem>();
            var backlightMain = backlightParticle.main;
            backlightMain.startColor = new ParticleSystem.MinMaxGradient(new Color(backlightMain.startColor.color.r,
                backlightMain.startColor.color.g, backlightMain.startColor.color.b, 0.1f));
        }
        
        public void IncreaseFireEffect4()
        {
            var fireParticle = fireEffect.GetComponent<ParticleSystem>();
            var main = fireParticle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0.2f));
            main.startSpeed = -2f;
            var backlightParticle = fireBacklight.GetComponent<ParticleSystem>();
            var backlightMain = backlightParticle.main;
            backlightMain.startColor = new ParticleSystem.MinMaxGradient(new Color(backlightMain.startColor.color.r,
                backlightMain.startColor.color.g, backlightMain.startColor.color.b, 0.05f));
        }

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetActiveMagnet(bool value)
        {
            magnetPlayer.gameObject.SetActive(value);
        }

        public void ChangeTheme()
        {
            meshFilter.mesh = visualController.GetPlayerMesh();
            meshRenderer.material = visualController.GetPlayerMaterial();
            
            //Destroy(trail);
            Destroy(fallingTrail);
            
            //trail = Instantiate(visualController.GetTrail(), transform.position, Quaternion.identity, transform);
            fallingTrail = Instantiate(visualController.GetFallingTrail(), new Vector3(0, 0.15f, -0.7f), Quaternion.identity,
                transform.parent);
        }

        public void TryOnPlayerSkin(Mesh _mesh, Material _material)
        {
            meshFilter.mesh = _mesh;
            meshRenderer.material = _material;
        }

        public void TryOnTrailSkin(GameObject _trail)
        {
            Destroy(trail);
            trail = Instantiate(_trail, transform.position, Quaternion.identity, transform);
        }

        public void TryOnFallingTrailSkin(GameObject _fallingTrail)
        {
            Destroy(fallingTrail);
            fallingTrail = Instantiate(_fallingTrail, new Vector3(0, 0.15f, -0.7f), Quaternion.identity,
                transform);
        }
        
        public void PlayIdleAnim()
        {
            animator.Play(IDLE_ANIMATION_IDENTIFIER);
        }

        public void DisableTrail()
        {
            trail.SetActive(false);
        }

        public void EnableTrail()
        {
            trail.SetActive(trail);
        }

        public void PlayBounceAnim()
        {
            animator.Play(BOUNCE_ANIMATION_IDENTIFIER);
        }

        public void EnableFallingTrail()
        {
            fallingTrail.SetActive(true);
        }

        public void DisableFallingTrail()
        {
            fallingTrail.SetActive(false);
        }

        public void SetTriggerStay(bool _value)
        {
            triggerStay = _value;
        }

        public void ContinueGame()
        {
            SetFallingTrailState();
        }

        public void MovePlatforms()
        {
            tubeMover.MoveTube(platformMover.platformMovementSpeed);
            platformMover.MovePlatforms();
        }
        
        private void Update()
        {
            if (triggerStay) return;
            
            var position = transform.position;
            var centerRay = new Ray(position, Vector3.down);
            
            if (Physics.Raycast(centerRay, out var centerHit, 0.105f))
            {
                if (!centerHit.transform.CompareTag(SEGMENT_TAG)) return;
                
                triggeredSegment = centerHit.collider.GetComponent<Segment>();
                platform = triggeredSegment.transform.parent.GetComponent<Platform>();
                /*if (platformMover.platformMovementSpeed >= 6 && segment.segmentData.segmentType != SegmentType.Hole && !bonusController.accelerationIsActive)
                {
                    //GetComponent<Animator>().Play("SpecialBounce");
                    SetTriggerStay(true);
                    //SetDefaultState();
                    
                    platformMover.DestroyPlatform(true);
                    
                    freeSpeedIncrease.ResetSpeed();
                    return;
                }*/
                
                switch (triggeredSegment.segmentData.segmentType)
                {
                    case SegmentType.Ground:
                        if (bonusController.accelerationIsActive)
                        {
                            SetTriggerStay(true);
                            platformMover.DestroyPlatform(true);
                            bonusController.StepAcceleration();
                        }
                        else
                        {
                            SetTriggerStay(true);
                            SetDefaultState();
                            if (gameManager.gameStarted) triggeredSegment.IncreasePlatformTouchCounter();
                            freeSpeedIncrease.ResetSpeed();
                            touchEffect = effectsPool.GetTouchEffect();
                            platform.touchEffect = touchEffect;
                            touchEffect.transform.position = new Vector3(centerHit.point.x, centerHit.point.y + 0.01f, centerHit.point.z);
                            touchEffect.transform.rotation = Quaternion.Euler(-90, 0, 0);
                            touchEffect.transform.SetParent(triggeredSegment.transform);
                            playerSounds.PlayTouchSound();
                            
                            playerParticles = effectsPool.GetPlayerParticles();
                            platform.playerParticles = playerParticles;
                            playerParticles.transform.position = new Vector3(centerHit.point.x,
                                centerHit.point.y + 0.01f, centerHit.point.z);
                            playerParticles.transform.rotation = Quaternion.identity;
                            playerParticles.transform.SetParent(triggeredSegment.transform);
                            platformSound.ResetPitch();
                        }
                        break;
                    case SegmentType.Hole:
                        if (!gameManager.gameStarted) return;
                        if (bonusController.accelerationIsActive)
                        {
                            SetTriggerStay(true);
                            platformMover.DestroyPlatform(true);
                            bonusController.StepAcceleration();
                        }
                        else
                        {
                            freeSpeedIncrease.IncreaseSpeed();
                            SetTriggerStay(true);
                            platformMover.DestroyPlatform(true);
                        }
                        break;
                    case SegmentType.Let:
                        if (!gameManager.gameStarted) return;
                        if (bonusController.accelerationIsActive)
                        {
                            SetTriggerStay(true);
                            platformMover.DestroyPlatform(true);
                            bonusController.StepAcceleration();
                            return;
                        }
                        if (bonusController.shieldIsActive)
                        {
                            bonusController.DeactivateBonus(BonusType.Shield);
                            SetTriggerStay(true);
                            freeSpeedIncrease.ResetSpeed();
                            SetDefaultState();
                            return;
                        }
                        platformSound.ResetPitch();
                        freeSpeedIncrease.ResetSpeed();
                        platformMover.Failed();
                        SetTriggerStay(true);
                        SetFailedState();
                        break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(BOUNCE_TRIGGER_TAG)) SetTriggerStay(false);
        }

        public void SetFailedState()
        {
            PlayIdleAnim();
            DisableFallingTrail();
            DisableTrail();
        }

        public void SetFallingTrailState()
        {
            PlayIdleAnim();
            EnableFallingTrail();
            DisableTrail();
        }

        public void SetDefaultState()
        {
            PlayBounceAnim();
            EnableTrail();
            DisableFallingTrail();
        }

        public void CollectCrystal(int count)
        {
            crystalPanel.AddCrystals(count);
            sessionData.AddCrystals(count);
        }
        
        public void CollectCoin(int count)
        {
            coinPanel.AddCoins(count);
            sessionData.AddCoins(count);
        }
        
        public void CollectKey(int count)
        {
            keyPanel.AddKeys(count);
            sessionData.AddKeys(count);
        }
    }
}

