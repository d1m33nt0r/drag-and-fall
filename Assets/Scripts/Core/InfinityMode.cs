using Data.Core;
using UnityEngine;

namespace Core
{
    public class InfinityMode : MonoBehaviour
    {
        public InfinityData infinityData;

        private int randPlatformsAmount;
        private bool randIsInitialized;
        private int shieldPositions;
        private int magnetPositions;
        private int multiplierPositions;
        private int accelerationPositions;
        private int keyPositions;
        private int setPointer;
        private int patternPointer;

        public void ResetPointers()
        {
            setPointer = 0;
            patternPointer = 0;
            randPlatformsAmount = 0;
            randIsInitialized = false;
        }
        
        public PatternData GetPatternData()
        {
            if (infinityData.patternSets[setPointer].isRandom)
            {
                if (!randIsInitialized)
                {
                    randPlatformsAmount = Random.Range(infinityData.patternSets[setPointer].minPlatformsCount,
                        infinityData.patternSets[setPointer].maxPlatformsCount);
                    
                    randIsInitialized = true;
                }
                
                if (randPlatformsAmount > patternPointer + 1)
                {
                    var patternData1 = new PatternData(12);
                    patternData1.isRandom = true;
                    patternData1.minHoleAmount = infinityData.patternSets[setPointer].minHoleAmount;
                    patternData1.maxHoleAmount = infinityData.patternSets[setPointer].maxHoleAmount;
                    patternData1.minLetAmount = infinityData.patternSets[setPointer].minLetAmount;
                    patternData1.maxLetAmount = infinityData.patternSets[setPointer].maxLetAmount;
                    patternPointer++;
                    return patternData1;
                }

                if (infinityData.patternSets.Count > setPointer + 1)
                {
                    var patternData2 = new PatternData(12);
                    patternData2.isRandom = true;
                    patternData2.minHoleAmount = infinityData.patternSets[setPointer].minHoleAmount;
                    patternData2.maxHoleAmount = infinityData.patternSets[setPointer].maxHoleAmount;
                    patternData2.minLetAmount = infinityData.patternSets[setPointer].minLetAmount;
                    patternData2.maxLetAmount = infinityData.patternSets[setPointer].maxLetAmount;
                    patternData2.isLast = true;
                    randIsInitialized = false;
                    patternPointer = 0;
                    setPointer++;
                    return patternData2;
                }

                var patternData3 = new PatternData(12);
                patternData3.isRandom = true;
                patternData3.minHoleAmount = infinityData.patternSets[setPointer].minHoleAmount;
                patternData3.maxHoleAmount = infinityData.patternSets[setPointer].maxHoleAmount;
                patternData3.minLetAmount = infinityData.patternSets[setPointer].minLetAmount;
                patternData3.maxLetAmount = infinityData.patternSets[setPointer].maxLetAmount;
                patternData3.isLast = true;
                randIsInitialized = false;
                patternPointer = 0;
                setPointer = 0;
                return patternData3;
            }
            else
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
}