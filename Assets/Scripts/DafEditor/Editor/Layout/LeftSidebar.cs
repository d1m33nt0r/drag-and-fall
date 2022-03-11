using DafEditor.Editor.Intefraces;
using Data.Core;
using UnityEditor;
using UnityEngine;
using EditorStyles = DafEditor.Editor.Common.EditorStyles;

namespace DafEditor.Editor.Layout
{
    public class LeftSidebar : IDrawer
    {
        public const string INFINITY_DATA_ASSET_PATH = "Assets/Resources/GameData/InfinityData.asset";
        public const string LEVELS_DATA_ASSET_PATH = "Assets/Resources/GameData/LevelsData.asset";

        public const string LEVELS_DATA_PATH = "Assets/Resources/GameData/Levels/";
        public const string INFINITY_DATA_PATH = "Assets/Resources/GameData/InfinitySets/";
        
        private const float MARGIN_TOP = 0;
        private int currentToolbarOption = 0;
        private string[] toolbarOptions = {"Infinity mode", "Levels"};
        
        public void Draw()
        {
            GUILayout.BeginArea(new Rect(new Vector2(0, GameEditorWindow.instance.m_topBar.TOP_BAR_HEIGHT), 
                new Vector2(GameEditorWindow.instance.m_splitLine.xSplitCoordinate - 
                            GameEditorWindow.instance.m_splitLine.lineWidth,GameEditorWindow.instance.position.height)), EditorStyles.LeftSidebarStyle());

            GUILayout.Label("Main", EditorStyles.HeaderOfBlockInLeftSideBar());

            GUILayout.Space(5f);
            currentToolbarOption = GUILayout.Toolbar(currentToolbarOption, toolbarOptions);
            GUILayout.Space(5f);

            if (currentToolbarOption == 0)
                DrawInfinityMode();
            else
                DrawLevels();

            GUILayout.EndArea();
        }

        private void DrawLevels()
        {
            var levelsData = AssetDatabase.LoadAssetAtPath<LevelsData>(LEVELS_DATA_ASSET_PATH);
            
            if (GUILayout.Button("Create new level", EditorStyles.DarkButtonStyle(22)))
            {
                var levelData = ScriptableObject.CreateInstance<LevelData>();
                var count = levelsData.leves.Count + 1;
                AssetDatabase.CreateAsset(levelData, LEVELS_DATA_PATH + "Level_" +  count + ".asset");
                levelsData.leves.Add(levelData);
            }
            
            GUILayout.BeginVertical("box");

            for (var i = 0; i < levelsData.leves.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Button(levelsData.leves[i].name, EditorStyles.DarkButtonStyle(22));
                GUILayout.Button("X", EditorStyles.RedButtonStyle(22), GUILayout.Width(25));
                GUILayout.EndHorizontal();
            }
            
            GUILayout.EndVertical();
        }
        
        private static void DrawInfinityMode()
        {
            var infinityData = AssetDatabase.LoadAssetAtPath<InfinityData>(INFINITY_DATA_ASSET_PATH);
            
            if (GUILayout.Button("Create new set", EditorStyles.DarkButtonStyle(22)))
            {
                var setData = ScriptableObject.CreateInstance<SetData>();
                var count = infinityData.patternSets.Count + 1;
                AssetDatabase.CreateAsset(setData, INFINITY_DATA_PATH + count + "_Set.asset");
                infinityData.patternSets.Add(setData);
            }
            
            GUILayout.BeginVertical("box");

            for (var i = 0; i < infinityData.patternSets.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Button(infinityData.patternSets[i].name, EditorStyles.DarkButtonStyle(22));
                GUILayout.Button("X", EditorStyles.RedButtonStyle(22), GUILayout.Width(25));
                GUILayout.EndHorizontal();
            }
            
            GUILayout.EndVertical();
        }
    }
}