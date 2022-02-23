using System;

namespace PatternManager
{
    [Serializable]
    public class Level
    {
        public SetData[] patternSets;
        public Level(int patternAmount) => patternSets = new SetData[patternAmount];
    }
}