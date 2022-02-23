using PatternManager.Editor.Intefraces;
using UnityEngine;
using EditorStyles = PatternManager.Editor.Common.EditorStyles;

namespace PatternManager.Editor
{
    public class TopBar : IDrawer
    {
        public readonly float TOP_BAR_HEIGHT = 23;
        
        public void Draw()
        {
            GUILayout.BeginArea(new Rect(new Vector2(0, 0), 
                new Vector2(GameEditorWindow.instance.position.width, TOP_BAR_HEIGHT)), EditorStyles.TopBarBackgroundStyle());

            var style = new GUIStyle {alignment = TextAnchor.MiddleCenter};

            GUILayout.BeginHorizontal();

            GUILayout.Button("Preset manager");

            GUILayout.Button("Theme manager");

            GUILayout.Button("Constants");
            
            GUILayout.EndHorizontal();
            
            GUILayout.EndArea();
        }
    }
}