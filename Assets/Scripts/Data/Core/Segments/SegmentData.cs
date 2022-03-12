﻿using System;
using Data.Core.Segments.Content;

namespace Data.Core.Segments
{
    [Serializable]
    public class SegmentData
    {
        public SegmentType segmentType = SegmentType.Ground;
        public SegmentContent segmentContent = SegmentContent.None;
    }
}