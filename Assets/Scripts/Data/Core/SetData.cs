using System.Collections.Generic;
using UnityEngine;

namespace Data.Core
{
    [CreateAssetMenu(fileName = "SetData", menuName = "Gamer Stash/Set Data", order = 0)]
    public class SetData : ScriptableObject
    {
        public bool isRandom;
        public int minPlatformsCount;
        public int maxPlatformsCount;
        public int maxLetAmount;
        public int minLetAmount;
        public int maxHoleAmount;
        public int minHoleAmount;
        public int attemptsOfShieldInstantiate;
        public int[] shieldPositions = new int[0];
        public bool spawnShieldOnLet;
        public int attemptsOfKeyInstantiate;
        public int[] keyPositions = new int[0];
        public bool spawnKeyOnLet;
        public int attemptsOfMagnetInstantiate;
        public int[] magnetPositions = new int[0];
        public bool spawnMagnetOnLet;
        public int attemptsOfMultiplierInstantiate;
        public int[] multiplierPositions = new int[0];
        public bool spawnMultiplierOnLet;
        public int attemptsOfAccelerationInstantiate;
        public int[] accelerationPositions = new int[0];
        public bool spawnAccelerationOnLet;
        public int maxCoinAmount;
        public int minCoinAmount;
        public AccuracyLevel coinAccuracyLevel;
        public AccuracyLevel crystalAccuracyLevel;
        public int maxCrystalAmount;
        public int minCrystalAmount;
        public List<PatternData> patterns;
    }
}