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
        public List<PatternData> patterns;
    }
}