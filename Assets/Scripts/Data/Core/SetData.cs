using System.Collections.Generic;
using Data.Core.Segments;
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
        public bool spawnShieldOnGround;
        public bool spawnShieldOnHole;
        public int spawnShieldChance;
        public int attemptsOfKeyInstantiate;
        public int[] keyPositions = new int[0];
        public bool spawnKeyOnLet;
        public bool spawnKeyOnGround;
        public bool spawnKeyOnHole;
        public int spawnKeyChance;
        public int attemptsOfMagnetInstantiate;
        public int[] magnetPositions = new int[0];
        public bool spawnMagnetOnLet;
        public bool spawnMagnetOnGround;
        public bool spawnMagnetOnHole;
        public int spawnMagnetChance;
        public int attemptsOfMultiplierInstantiate;
        public int[] multiplierPositions = new int[0];
        public bool spawnMultiplierOnLet;
        public bool spawnMultiplierOnGround;
        public bool spawnMultiplierOnHole;
        public int spawnMultiplierChance;
        public int attemptsOfAccelerationInstantiate;
        public int[] accelerationPositions = new int[0];
        public bool spawnAccelerationOnLet;
        public bool spawnAccelerationOnGround;
        public bool spawnAccelerationOnHole;
        public int spawnAccelerationChance;
        public CurrencyRandomSettings[] currencyRandomSettings;

        public List<PatternData> patterns;
    }
}