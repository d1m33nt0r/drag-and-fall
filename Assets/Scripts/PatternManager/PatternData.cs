using System;

namespace PatternManager
{
    [Serializable]
    public class PatternData
    {
        public SegmentData[] segmentsData;

        public int index;
        
        public PatternData(int _segmentsAmount, int _index)
        {
            segmentsData = new SegmentData[_segmentsAmount];
            index = _index;
            
            for (int i = 0; i < _segmentsAmount; i++)
            {
                segmentsData[i] = new SegmentData
                {
                    bonusType = BonusType.None,
                    segmentType = SegmentType.Ground
                };
            }
        }
    }
}