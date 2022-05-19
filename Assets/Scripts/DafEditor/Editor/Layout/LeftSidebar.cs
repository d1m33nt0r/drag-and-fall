using System.Collections.Generic;
using DafEditor.Editor.Common;
using DafEditor.Editor.Intefraces;
using Data.Core;
using UnityEditor;
using UnityEngine;
using EditorStyles = DafEditor.Editor.Common.EditorStyles;

namespace DafEditor.Editor.Layout
{
    public class LeftSidebar : IDrawer
    {
        public string INFINITY_DATA_ASSET_PATH = "Assets/Resources/GameData/InfinityData.asset";
        public string LEVELS_DATA_ASSET_PATH = "Assets/Resources/GameData/LevelsData.asset";

        public string LEVELS_DATA_PATH = "Assets/Resources/GameData/Levels/";
        public string INFINITY_DATA_PATH = "Assets/Resources/GameData/InfinitySets/";
        
        private const float MARGIN_TOP = 0;
        private int currentToolbarOption = 0;
        private string[] toolbarOptions = {"Infinity mode", "Levels"};
        
        public void Draw()
        {
            GUILayout.BeginArea(new Rect(new Vector2(0, 0), 
                new Vector2(GameEditorWindow.instance.leftSplitLine.xSplitCoordinate - 
                            GameEditorWindow.instance.leftSplitLine.lineWidth,GameEditorWindow.instance.position.height)), EditorStyles.LeftSidebarStyle());

            GUILayout.Label("Manage patterns", EditorStyles.HeaderOfBlockInLeftSideBar());

            GUILayout.Space(5f);
            currentToolbarOption = GUILayout.Toolbar(currentToolbarOption, toolbarOptions);
            GUILayout.Space(5f);

            if (currentToolbarOption == 0)
            {
                GameEditorWindow.instance.state = EditorState.Infinity;
                DrawInfinityMode();
            }
            else
            {
                GameEditorWindow.instance.state = EditorState.Levels;
                DrawLevels();
            }

            GUILayout.EndArea();
        }

        private void DrawLevels()
        {
            GameEditorWindow.instance.SetRightPanelState(RightPanelState.Empty);
            
            GameEditorWindow.instance.SetCurrentRandomSetData(null);
            var levelsData = AssetDatabase.LoadAssetAtPath<LevelsData>(LEVELS_DATA_ASSET_PATH);
            
            if (GUILayout.Button("Create new level", EditorStyles.DarkButtonStyle(22)))
            {
                var levelData = ScriptableObject.CreateInstance<LevelData>();
                levelData.levelIndex = levelsData.leves.Count;
                levelData.patterns = new List<PatternData>();
                var count = levelsData.leves.Count + 1;
                AssetDatabase.CreateAsset(levelData, LEVELS_DATA_PATH + "Level_" +  count + ".asset");
                EditorUtility.SetDirty(levelsData);
                levelsData.leves.Add(levelData);
            }
            
            GUILayout.BeginVertical("box");

            for (var i = 0; i < levelsData.leves.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(levelsData.leves[i].name, EditorStyles.DarkButtonStyle(22)))
                {
                    GameEditorWindow.instance.currentPatternsData = levelsData.leves[i].patterns;
                    GameEditorWindow.instance.content.SetPatterns(GameEditorWindow.instance.currentPatternsData, levelsData.leves[i].name);
                    GameEditorWindow.instance.currentLevelData = levelsData.leves[i];
                }
                GUILayout.Button("X", EditorStyles.RedButtonStyle(22), GUILayout.Width(25));
                GUILayout.EndHorizontal();
            }
            
            GUILayout.EndVertical();
        }
        
        private void DrawInfinityMode()
        {
            var infinityData = AssetDatabase.LoadAssetAtPath<InfinityData>(INFINITY_DATA_ASSET_PATH);
            
            if (GUILayout.Button("Create new set", EditorStyles.DarkButtonStyle(22)))
            {
                var setData = ScriptableObject.CreateInstance<SetData>();
                setData.patterns = new List<PatternData>();
                var count = infinityData.patternSets.Count + 1;
                AssetDatabase.CreateAsset(setData, INFINITY_DATA_PATH + count + "_Set.asset");
                infinityData.patternSets.Add(setData);
                EditorUtility.SetDirty(infinityData);
            }
            
            GUILayout.BeginVertical("box");

            for (var i = 0; i < infinityData.patternSets.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(infinityData.patternSets[i].isRandom ? "R" : "", 
                    EditorStyles.RedButtonStyle(22), GUILayout.Width(25)))
                {
                    if (Event.current.button == 0)
                    {
                        infinityData.patternSets[i].isRandom = !infinityData.patternSets[i].isRandom;
                        if (!infinityData.patternSets[i].isRandom)
                        {
                            GameEditorWindow.instance.SetRightPanelState(RightPanelState.Empty);
                            GameEditorWindow.instance.SetCurrentRandomPatternData(null);
                            GameEditorWindow.instance.SetCurrentRandomSetData(null);
                            infinityData.patternSets[i].isRandom = false;
                        }
                        else
                        {
                            GameEditorWindow.instance.SetRightPanelState(RightPanelState.SetRandomSettings);
                            GameEditorWindow.instance.SetCurrentRandomPatternData(null);
                            GameEditorWindow.instance.SetCurrentRandomSetData(infinityData.patternSets[i]);
                            infinityData.patternSets[i].isRandom = true;
                        }
                    }
                    if (Event.current.button == 2)
                    {
                        if (infinityData.patternSets[i].isRandom)
                        {
                            GameEditorWindow.instance.SetRightPanelState(RightPanelState.SetRandomSettings);
                            GameEditorWindow.instance.SetCurrentRandomPatternData(null);
                            GameEditorWindow.instance.SetCurrentRandomSetData(infinityData.patternSets[i]);
                        }
                        else
                        {
                            GameEditorWindow.instance.SetCurrentRandomPatternData(null);
                            GameEditorWindow.instance.SetCurrentRandomSetData(null);
                            GameEditorWindow.instance.SetRightPanelState(RightPanelState.Empty);
                        }
                    }
                    
                    EditorUtility.SetDirty(infinityData.patternSets[i]);
                }

                if (GUILayout.Button("↑", EditorStyles.RedButtonStyle(22), GUILayout.Width(25)))
                {
                    
                }
                
                if (GUILayout.Button("↓", EditorStyles.RedButtonStyle(22), GUILayout.Width(25)))
                {
                    
                }
                
                if (GUILayout.Button(infinityData.patternSets[i].name, EditorStyles.DarkButtonStyle(22)))
                {
                    GameEditorWindow.instance.currentPatternsData = infinityData.patternSets[i].patterns;
                    GameEditorWindow.instance.currentSetData = infinityData.patternSets[i];
                    GameEditorWindow.instance.content.SetPatterns(GameEditorWindow.instance.currentPatternsData, infinityData.patternSets[i].name);
                }

                if (GUILayout.Button("D", EditorStyles.RedButtonStyle(22), GUILayout.Width(25)))
                {
                    var setData = Object.Instantiate(infinityData.patternSets[i]);
                    setData.patterns = new List<PatternData>();
                    var count = infinityData.patternSets.Count + 1;
                    AssetDatabase.CreateAsset(setData, INFINITY_DATA_PATH + count + "_Set.asset");
                    infinityData.patternSets.Add(setData);
                    EditorUtility.SetDirty(infinityData);
                }
                
                if (GUILayout.Button("X", EditorStyles.RedButtonStyle(22), GUILayout.Width(25)))
                {
                    AssetDatabase.DeleteAsset(INFINITY_DATA_PATH + infinityData.patternSets[i].name + ".asset");
                    infinityData.patternSets.RemoveAt(i);
                    EditorUtility.SetDirty(infinityData);
                }
                
                GUILayout.EndHorizontal();
            }
            
            GUILayout.EndVertical();
        }
    }
}