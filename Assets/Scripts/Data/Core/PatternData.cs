using System;
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
        }
    }
}