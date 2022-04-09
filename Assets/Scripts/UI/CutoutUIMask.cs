using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Core
{
    public class CutoutUIMask : Image
    {
        public override Material materialForRendering
        {
            get
            {
                var material = new Material(base.materialForRendering);
                material.SetInt("_StencilComp", (int) CompareFunction.NotEqual);
                return material;
            }
        }
    }
}