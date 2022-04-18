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
        
        public Platform GetPlatform()
        {
            if (platformPool.Count > 0) return platformPool.Dequeue();
            
            var platformInstance = Instantiate(platformPrefab, transform);
            platformInstance.Construct(platformMover, player, bonusController, gainScore, segmentContentPool, tubeMover, tutorialUI, platformSound);
            return platformInstance;
        }
    }
}