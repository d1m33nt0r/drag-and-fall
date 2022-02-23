using System;
using Data.Core.Segments.Content;

namespace Data.Core.Segments
{
    [Serializable]
    public class SegmentData
    {
        public SegmentType segmentType;
        public SegmentContent segmentContent;
    }
}