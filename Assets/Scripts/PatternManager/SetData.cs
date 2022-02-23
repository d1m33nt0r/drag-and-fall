using System;

namespace PatternManager
{
    [Serializable]
    public class SetData
    {
        public PatternData[] patterns;
        public SetData(int patternsAmount)
        {
            patterns = new PatternData[patternsAmount];
            
            for (var i = 0; i < patternsAmount; i++)
                patterns[i] = new PatternData(12, i);
        }
    }
}