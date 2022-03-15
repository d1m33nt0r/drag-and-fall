using Data.Core;
using UnityEngine;

namespace Core
{
    public class InfinityMode : MonoBehaviour
    {
        public InfinityData infinityData;

        private int setPointer;
        private int patternPointer;

        public void ResetPointers()
        {
            setPointer = 0;
            patternPointer = 0;
        }
        
        public PatternData GetPatternData()
        {
            var patternData = infinityData.patternSets[setPointer].patterns[patternPointer];

            if (infinityData.patternSets[setPointer].patterns.Count > patternPointer + 1)
            {
                patternPointer++;
                return patternData;
            }

            if (infinityData.patternSets.Count > setPointer + 1)
            {
                patternPointer = 0;
                setPointer++;
                return patternData;
            }

            patternPointer = 0;
            setPointer = 0;
            return patternData;
        }
    }
}