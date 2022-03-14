using Data.Core;
using UnityEngine;

namespace Core
{
    public class LevelMode : MonoBehaviour
    {
        public LevelData level;

        private int patternPointer;
        
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
                patternPointer++;
                return patternData;
            }
            
            if (level.patterns.Count == patternPointer + 1)
            {
                var patternData = level.patterns[patternPointer];
                patternPointer++;
                return patternData;
            }
            
            //ResetPointer();
            return null;
        }
    }
}