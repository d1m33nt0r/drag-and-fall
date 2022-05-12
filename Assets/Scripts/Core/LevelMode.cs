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

        [Inject] private Queue<GamePatternData> patternDataPool;

        public void SetLevelData(LevelData _levelData)
        {
            level = _levelData;
            ResetPointer();
        }

        public void ResetPointer()
        {
            patternPointer = 0;
        }

        public GamePatternData GetPatternData()
        {
            if (level.patterns.Count > patternPointer + 1)
            {
                var patternData = level.patterns[patternPointer];
                var gamePatternData = patternDataPool.Dequeue();
                gamePatternData.Configure(patternData);
                patternPointer++;
                return gamePatternData;
            }

            if (level.patterns.Count == patternPointer + 1)
            {
                var patternData = level.patterns[patternPointer];
                var gamePatternData = patternDataPool.Dequeue();
                gamePatternData.Configure(patternData);
                patternPointer++;
                return gamePatternData;
            }

            //ResetPointer();
            return null;
        }
    }
}