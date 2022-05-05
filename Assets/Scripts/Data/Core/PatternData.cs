using System;
using System.Collections.Generic;
using Data.Core.Segments;
using Data.Core.Segments.Content;

namespace Data.Core
{
    [Serializable]
    public class PatternData
    {
        public bool isRandom;
        public int maxLetAmount;
        public int minLetAmount;
        public int maxHoleAmount;
        public int minHoleAmount;
        public bool isLast;
        public float segmentRotationBias;
        public SegmentData[] segmentsData;
        private Queue<PatternData> patternDataPool;
        
        public PatternData(int _segmentsAmount, Queue<PatternData> patternDataPool)
        {
            segmentsData = new SegmentData[_segmentsAmount];
            this.patternDataPool = patternDataPool;
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