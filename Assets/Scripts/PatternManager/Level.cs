using System;

namespace PatternManager
{
    [Serializable]
    public class Level
    {
        public Set[] patternSets;
        public Level(int patternAmount) => patternSets = new Set[patternAmount];
    }
}