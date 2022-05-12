using System;
using System.Collections.Generic;
using Data.Core.Segments;
using Data.Core.Segments.Content;

namespace Data.Core
{
    [Serializable]
    public class GamePatternData
    {
        public bool isRandom;
        public int maxLetAmount;
        public int minLetAmount;
        public int maxHoleAmount;
        public int minHoleAmount;
        public bool isLast;
        public float segmentRotationBias;
        public SegmentData[] segmentsData;
        private Queue<GamePatternData> patternDataPool;

        public void InjectPatternDataPool(Queue<GamePatternData> patternDataPool)
        {
            this.patternDataPool = patternDataPool;
        }
        
        public GamePatternData(int _segmentsAmount, Queue<GamePatternData> patternDataPool)
        {
            segmentsData = new SegmentData[_segmentsAmount];
            this.patternDataPool = patternDataPool;
        }

        public void Configure(PatternData patternData)
        {
            isLast = patternData.isLast;
            isRandom = patternData.isRandom;
            maxHoleAmount = patternData.maxHoleAmount;
            maxLetAmount = patternData.maxLetAmount;
            minHoleAmount = patternData.minHoleAmount;
            minLetAmount = patternData.minLetAmount;
            segmentRotationBias = patternData.segmentRotationBias;
            
            for (var i = 0; i < patternData.segmentsData.Length; i++)
            {
                segmentsData[i].segmentContent = patternData.segmentsData[i].segmentContent;
                segmentsData[i].segmentType = patternData.segmentsData[i].segmentType;
            }
        }
        
        public void ReturnToPool()
        {
            isLast = default;
            isRandom = default;
            maxHoleAmount = default;
            maxLetAmount = default;
            minHoleAmount = default;
            minLetAmount = default;
            segmentRotationBias = default;
            
            for (var i = 0; i < segmentsData.Length; i++)
            {
                segmentsData[i].segmentContent = SegmentContent.None;
                segmentsData[i].segmentType = SegmentType.Ground;
            }
            
            patternDataPool.Enqueue(this);
        }
    }
}