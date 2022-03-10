using System;
using Data.Core.Segments;

namespace Data.Core
{
    [Serializable]
    public class PatternData
    {
        public SegmentData[] segmentsData;
        
        
        public PatternData(int _segmentsAmount)
        {
            segmentsData = new SegmentData[_segmentsAmount];
            
            /*for (int i = 0; i < _segmentsAmount; i++)
            {
                segmentsData[i] = new SegmentData
                {
                    bonusType = BonusType.None,
                    segmentType = SegmentType.Ground
                };
            }*/
        }
    }
}