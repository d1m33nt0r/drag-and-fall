using System.Collections.Generic;
using DafEditor.Editor.Common;
using DafEditor.Editor.Layout;
using Data.Core;
using UnityEditor;

namespace DafEditor.Editor
{
    public class GameEditorWindow : EditorWindowSingleton<GameEditorWindow>
    {
        protected override string customTitle => "DAF Editor";

        private LevelsData levelsData;
        private InfinityData infinityData;

        public List<PatternData> currentPatternData;
        
        public SplitLine m_splitLine;
        public LeftSidebar m_leftSidebar;
        public CenterPoint centerPoint;
        public TopBar m_topBar;
        public TopBarSplitLine m_topBarSplitLine;
        public Content m_content;
        
        [MenuItem("Tools/DAF Editor")]
        private static void ShowWindow()
        {
            var window = instance;
        }

        private void OnEnable()
        {
            centerPoint = new CenterPoint();
            m_topBarSplitLine = new TopBarSplitLine();
            m_splitLine = new SplitLine();
            m_content = new Content();
            m_leftSidebar = new LeftSidebar();
            m_topBar = new TopBar();
            
            infinityData = AssetDatabase.LoadAssetAtPath<InfinityData>(m_leftSidebar.INFINITY_DATA_ASSET_PATH);
            levelsData = AssetDatabase.LoadAssetAtPath<LevelsData>(m_leftSidebar.LEVELS_DATA_ASSET_PATH);
            
            
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            m_splitLine.Draw();
            m_leftSidebar.Draw();
            m_content.Draw();
            m_topBar.Draw();
            m_topBarSplitLine.Draw();
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(infinityData);
                EditorUtility.SetDirty(levelsData);
                
                foreach (var setData in infinityData.patternSets) EditorUtility.SetDirty(setData);
                foreach (var levelData in levelsData.leves) EditorUtility.SetDirty(levelData);

                AssetDatabase.SaveAssets();
            }
        }
    }
}