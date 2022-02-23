using System.Collections.Generic;
using UnityEngine;

namespace PatternManager
{
    [CreateAssetMenu(menuName = "PatternStorage", fileName = "PatternStorage")]
    public class PatternStorage : ScriptableObject
    {
        public List<Set> infinitySets = new List<Set>();
        public List<Level> levels = new List<Level>();
    }
}