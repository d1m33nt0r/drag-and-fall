using System;
using Core.Bonuses;
using Data;
using Data.Core.Segments;
using ObjectPool;
using UI.InfinityUI;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public event Action failed;

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
        
        private bool triggerStay;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        public void SetActiveFireEffect(bool value)
        {
            fireEffect.SetActive(value);
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
            var fireParticle = fireEffect.GetComponent<ParticleSystem>();
            var main = fireParticle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 1, 1, 1f), new Color(1, 1, 1, 0.5f));
            main.startSpeed = -3;

        }

        public void IncreaseFireEffect5()
        {
            var fireParticle = fireEffect.GetComponent<ParticleSystem>();
            var main = fireParticle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 1, 1, 0.25f), new Color(1, 1, 1, 0.2f));
            main.startSpeed = -2.5f;
        }
        
        public void IncreaseFireEffect4()
        {
            var fireParticle = fireEffect.GetComponent<ParticleSystem>();
            var main = fireParticle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0.2f));
            main.startSpeed = -2f;
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
            
            Destroy(trail);
            Destroy(fallingTrail);
            
            trail = Instantiate(visualController.GetTrail(), transform.position, Quaternion.identity, transform);
            fallingTrail = Instantiate(visualController.GetFallingTrail(), transform.position, Quaternion.identity,
                transform);
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
            fallingTrail = Instantiate(_fallingTrail, transform.position, Quaternion.identity,
                transform);
        }
        
        public void PlayIdleAnim()
        {
            animator.Play("Idle");
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
            animator.Play("Bounce");
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
            tubeMover.MoveTube();
            platformMover.MovePlatforms();
        }
        
        private void Update()
        {
            if (triggerStay) return;
            
            var position = transform.position;
            var centerRay = new Ray(position, Vector3.down);
            
            if (Physics.Raycast(centerRay, out var centerHit, 0.105f))
            {
                if (!centerHit.transform.CompareTag("Segment")) return;
                
                var segment = centerHit.collider.GetComponent<Segment>();
                
                if (platformMover.platformMovementSpeed == 6 && segment.segmentData.segmentType != SegmentType.Hole && !bonusController.accelerationIsActive)
                {
                    //GetComponent<Animator>().Play("SpecialBounce");
                    SetTriggerStay(true);
                    //SetDefaultState();
                    
                    platformMover.DestroyPlatform(true);
                    
                    freeSpeedIncrease.ResetSpeed();
                    return;
                }
                
                switch (segment.segmentData.segmentType)
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
                            if (gameManager.gameStarted) segment.IncreasePlatformTouchCounter();
                            freeSpeedIncrease.ResetSpeed();
                            var instance = Instantiate(sled, new Vector3(centerHit.point.x, centerHit.point.y + 0.01f, centerHit.point.z), Quaternion.Euler(-90, 0, 0), segment.transform);//effectsPool.GetTouchEffect();
                            //instance.gameObject.SetActive(true);
                            //instance.transform.position = new Vector3(centerHit.point.x, centerHit.point.y + 0.01f, centerHit.point.z);
                            //instance.transform.rotation = Quaternion.Euler(-90, 0, 0);
                            //instance.transform.SetParent(segment.transform);
                                //Instantiate(sled, new Vector3(centerHit.point.x, centerHit.point.y + 0.01f, centerHit.point.z), Quaternion.Euler(-90, 0, 0), segment.transform);
                            instance.GetComponent<Animator>().Play("Touch");
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
            if (other.CompareTag("BounceTrigger")) SetTriggerStay(false);
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

