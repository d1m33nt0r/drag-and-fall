using System;
using System.Collections.Generic;
using Core;
using Core.Bonuses;
using Sound;
using UI;
using UnityEngine;

namespace ObjectPool
{
    public class PlatformPool : MonoBehaviour
    {
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private Player player;
        [SerializeField] private BonusController bonusController;
        [SerializeField] private GainScore gainScore;
        [SerializeField] private SegmentContentPool segmentContentPool;
        [SerializeField] private TubeMover tubeMover;
        [SerializeField] private TutorialUI tutorialUI;
        [SerializeField] private PlatformSound platformSound;
        
        [SerializeField] private Platform platformPrefab;
        [SerializeField] private int poolSize;

        private Queue<Platform> platformPool = new Queue<Platform>();

        public void Awake()
        {
            for (var i = 0; i < poolSize; i++)
            {
                var platformInstance = Instantiate(platformPrefab, transform);
                platformInstance.Construct(this, platformMover, player, bonusController, gainScore, segmentContentPool, tubeMover, tutorialUI, platformSound);
                platformPool.Enqueue(platformInstance);
            }
        }

        public void ChangeTheme(string themeIdentifier)
        {
            foreach (var platform in platformPool)
            {
                platform.ChangeTheme(themeIdentifier);
            }
        }

        public Platform GetPlatform()
        {
            if (platformPool.Count > 0)
            {
                var platform = platformPool.Dequeue();
                for (var i = 0; i < 12; i++)
                {
                    platform.segments[i].GetComponent<MeshCollider>().enabled = true;
                }
                return platform;
            }
            
            var platformInstance = Instantiate(platformPrefab, transform);
            platformInstance.Construct(this, platformMover, player, bonusController, gainScore, segmentContentPool, tubeMover, tutorialUI, platformSound);
            return platformInstance;
        }

        public void ReturnToPool(Platform platform)
        {
            platform.countTouches = 0;
            platform.transform.SetParent(transform);
            platformPool.Enqueue(platform);
        }
    }
}