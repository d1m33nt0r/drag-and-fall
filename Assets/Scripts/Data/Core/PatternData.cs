using System;
using Data.Core.Segments;
using Data.Core.Segments.Content;

namespace Data.Core
{
    [Serializable]
    public struct PatternData
    {
        public bool isRandom;
        public int maxLetAmount;
        public int minLetAmount;
        public int maxHoleAmount;
        public int minHoleAmount;
        public bool isLast;
        public float segmentRotationBias;
        public SegmentData[] segmentsData;
        public bool initialized;
        
        public PatternData(int _segmentsAmount)
        {
            segmentsData = new SegmentData[_segmentsAmount];
            
            for (var i = 0; i < _segmentsAmount; i++)
            {
                segmentsData[i] = new SegmentData
                {
                    segmentContent = SegmentContent.None,
                    segmentType = SegmentType.Ground
                };
            }

            isRandom = default;
            maxHoleAmount = default;
            minHoleAmount = default;
            maxLetAmount = default;
            minLetAmount = default;
            isLast = default;
            segmentRotationBias = default;
            initialized = false;
        }
    }
}