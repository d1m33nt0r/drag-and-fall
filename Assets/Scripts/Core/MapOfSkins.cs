using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "MapOfMaterials", menuName = "Map of Materials")]
    public class MapOfSkins : ScriptableObject
    {
        public int[] themeIndexes;
        public int[] skinIndexes;

        public Material[] materials;
        
        public Dictionary<string, Material> Skin => new Dictionary<string, Material>
        {
            {"00", materials[0]},
            {"01", materials[1]},
            {"02", materials[2]},
            {"03", materials[3]},
            {"10", materials[4]},
            {"11", materials[5]},
            {"12", materials[6]},
            {"13", materials[7]},
            {"20", materials[8]},
            {"21", materials[9]},
            {"22", materials[10]},
            {"23", materials[11]},
            {"30", materials[12]},
            {"31", materials[13]},
            {"32", materials[14]},
            {"33", materials[15]},
        };
    }
}