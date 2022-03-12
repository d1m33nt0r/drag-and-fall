using System;
using Data.Core.Segments;
using Data.Core.Segments.Content;

namespace Data.Core
{
    [Serializable]
    public class PatternData
    {
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