using System;

namespace PatternManager
{
    [Serializable]
    public class Set
    {
        public Pattern[] patterns;
        public Set(int patternsAmount)
        {
            patterns = new Pattern[patternsAmount];
            
            for (var i = 0; i < patternsAmount; i++)
                patterns[i] = new Pattern(12, i);
        }
    }
}