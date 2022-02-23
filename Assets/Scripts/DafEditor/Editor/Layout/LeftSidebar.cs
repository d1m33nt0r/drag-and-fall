using DafEditor.Editor.Intefraces;
using UnityEngine;
using EditorStyles = DafEditor.Editor.Common.EditorStyles;

namespace DafEditor.Editor.Layout
{
    public class LeftSidebar : IDrawer
    {
        private const float MARGIN_TOP = 0;
        
        public void Draw()
        {
            GUILayout.BeginArea(new Rect(new Vector2(0, GameEditorWindow.instance.m_topBar.TOP_BAR_HEIGHT), 
                new Vector2(GameEditorWindow.instance.m_splitLine.xSplitCoordinate - 
                            GameEditorWindow.instance.m_splitLine.lineWidth,GameEditorWindow.instance.position.height)), EditorStyles.LeftSidebarStyle());

            GUILayout.Label("Main", EditorStyles.HeaderOfBlockInLeftSideBar());

            GUILayout.Space(5f);
            GUILayout.Toolbar(0, new[] {"Infinity presets", "Level presets"});
            GUILayout.Space(5f);
            
            GUILayout.Button("Create new set", EditorStyles.DarkButtonStyle(22));
            
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            GUILayout.Button("Set 1", EditorStyles.DarkButtonStyle(22));
            GUILayout.Button("X", EditorStyles.RedButtonStyle(22), GUILayout.Width(25));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Button("Set 2", EditorStyles.DarkButtonStyle(22));
            GUILayout.Button("X", EditorStyles.RedButtonStyle(22), GUILayout.Width(25));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Button("Set 3", EditorStyles.DarkButtonStyle(22));
            GUILayout.Button("X", EditorStyles.RedButtonStyle(22), GUILayout.Width(25));
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Button("Set 4", EditorStyles.DarkButtonStyle(22));
            GUILayout.Button("X", EditorStyles.RedButtonStyle(22), GUILayout.Width(25));
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();
            
            GUILayout.EndArea();
        }
    }
}