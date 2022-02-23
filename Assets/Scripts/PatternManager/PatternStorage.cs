using System.Collections.Generic;
using UnityEngine;

namespace PatternManager
{
    [CreateAssetMenu(menuName = "PatternStorage", fileName = "PatternStorage")]
    public class PatternStorage : ScriptableObject
    {
        public List<SetData> infinitySets = new List<SetData>();
        public List<Level> levels = new List<Level>();
    }
}