using Data;
using UnityEditor;

namespace Editor
{
    //[CustomEditor(typeof(ShopData))]
    public class ShopDataInspector : UnityEditor.Editor
    {
        private ShopData script;

        public override void OnInspectorGUI()
        {
            script = (ShopData) target;

            EditorGUILayout.BeginVertical();

            foreach (var environmentSkin in script.EnvironmentSkinData)
            {
         
            }
            
            EditorGUILayout.EndVertical();
        }
    }
}