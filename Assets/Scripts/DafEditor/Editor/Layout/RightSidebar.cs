using DafEditor.Editor.Common;
using DafEditor.Editor.Intefraces;
using UnityEditor;
using UnityEngine;

namespace DafEditor.Editor.Layout
{
    public class RightSidebar : LayoutComponent, IDrawer
    {
        private int minHoleAmount;
        private int maxHoleAmount;
        private int minLetAmount;
        private int maxLetAmount;
        
        public void Draw()
        {
            if (!isInitialized) return;
            
            GUILayout.BeginArea(new Rect(new Vector2(gameEditorWindow.position.size.x - 249, 0),
                new Vector2(250,
                    gameEditorWindow.position.height)), Common.EditorStyles.LeftSidebarStyle());

            GUILayout.Label("Random settings", Common.EditorStyles.HeaderOfBlockInLeftSideBar());

            switch (gameEditorWindow.currentRightPanelState)
            {
                case RightPanelState.BonusRandomSettings:
                    GUILayout.Label("Bonus random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar());
                    
                    break;
                case RightPanelState.PlatformRandomSettings:
                    GUILayout.Label("Platform random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar());
                    GUILayout.BeginVertical();
                    
                    EditorGUI.BeginChangeCheck();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Min hole amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentRandomPatternData.minHoleAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentRandomPatternData.minHoleAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Max hole amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentRandomPatternData.maxHoleAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentRandomPatternData.maxHoleAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Min let amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentRandomPatternData.minLetAmount = 
                        EditorGUILayout.IntSlider( gameEditorWindow.currentRandomPatternData.minLetAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Max hole amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentRandomPatternData.maxLetAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentRandomPatternData.maxLetAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorUtility.SetDirty(gameEditorWindow.infinityData.patternSets[0]);
                    }
                    GUILayout.EndVertical();
                    break;
                case RightPanelState.SegmentRandomSettings:
                    GUILayout.Label("Segment random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar());
                    break;
                case RightPanelState.SetRandomSettings:
                    GUILayout.Label("Set random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar());
                    GUILayout.BeginVertical();
                    
                    EditorGUI.BeginChangeCheck();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Min platforms");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentSetData.minPlatformsCount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.minPlatformsCount, 0, 50);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Max platforms");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentSetData.maxPlatformsCount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.maxPlatformsCount, 0, 50);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Min hole amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentSetData.minHoleAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.minHoleAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Max hole amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentSetData.maxHoleAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.maxHoleAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Min let amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentSetData.minLetAmount = 
                        EditorGUILayout.IntSlider( gameEditorWindow.currentSetData.minLetAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Max let amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentSetData.maxLetAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.maxLetAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorUtility.SetDirty(gameEditorWindow.infinityData.patternSets[0]);
                    }
                    break;
            }
            
            GUILayout.EndArea();
        }

        public void DrawBonusRandomSettings()
        {
            
        }
        
    }
}