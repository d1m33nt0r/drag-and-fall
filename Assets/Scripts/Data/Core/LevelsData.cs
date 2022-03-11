using System.Collections.Generic;
using UnityEngine;

namespace Data.Core
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Gamer Stash/Levels Data", order = 0)]
    public class LevelsData : ScriptableObject
    {
        public List<LevelData> leves;
    }
}