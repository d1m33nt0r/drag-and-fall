using System.Collections.Generic;
using Data.Core;
using UnityEngine;
using Zenject;

namespace Core
{
    public class LevelMode : MonoBehaviour
    {
        public LevelData level;

        private int patternPointer;

        [Inject] private Queue<PatternData> patternDataPool;

        public void SetLevelData(LevelData _levelData)
        {
            level = _levelData;
            ResetPointer();
        }

        public void ResetPointer()
        {
            patternPointer = 0;
        }

        public PatternData GetPatternData()
        {
            if (level.patterns.Count > patternPointer + 1)
            {
                var patternData = level.patterns[patternPointer];
                patternData.InjectPatternDataPool(patternDataPool);
                patternPointer++;
                return patternData;
            }

            if (level.patterns.Count == patternPointer + 1)
            {
                var patternData = level.patterns[patternPointer];
                patternData.InjectPatternDataPool(patternDataPool);
                patternPointer++;
                return patternData;
            }

            //ResetPointer();
            return null;
        }
    }
}