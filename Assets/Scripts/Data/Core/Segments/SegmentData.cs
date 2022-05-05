using System;
using Data.Core.Segments.Content;

namespace Data.Core.Segments
{
    [Serializable]
    public struct SegmentData
    {
        public int positionIndex;
        public SegmentType segmentType;
        public SegmentContent segmentContent;
    }
}