using System;

namespace PatternManager
{
    [Serializable]
    public class Pattern
    {
        public Segment[] segments;

        public int index;
        
        public Pattern(int _segmentsAmount, int _index)
        {
            segments = new Segment[_segmentsAmount];
            index = _index;
            
            for (int i = 0; i < _segmentsAmount; i++)
            {
                segments[i] = new Segment
                {
                    bonusType = BonusType.None,
                    segmentType = SegmentType.Ground
                };
            }
        }
    }
}