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

        public LevelsData levelsData;
        public InfinityData infinityData;

        public List<PatternData> currentPatternsData;
        
        public LeftSplitLine leftSplitLine;
        public RightSplitLine rightSpitLine;
        public LeftSidebar leftSidebar;
        public CenterPoint centerPoint;
        public RightSidebar rightSidebar;
        public Content content;

        public SetData currentSetData;
        public PatternData currentRandomPatternData;
        

        public RightPanelState currentRightPanelState;
        
        [MenuItem("Tools/DAF Editor")]
        private static void ShowWindow()
        {
            var window = instance;
        }

        private void OnEnable()
        {
            centerPoint = new CenterPoint();
            leftSplitLine = new LeftSplitLine();
            content = new Content();
            content.Construct(this);
            leftSidebar = new LeftSidebar();
            rightSpitLine = new RightSplitLine();
            rightSpitLine.Construct(this);
            rightSidebar = new RightSidebar();
            rightSidebar.Construct(this);
            infinityData = AssetDatabase.LoadAssetAtPath<InfinityData>(leftSidebar.INFINITY_DATA_ASSET_PATH);
            levelsData = AssetDatabase.LoadAssetAtPath<LevelsData>(leftSidebar.LEVELS_DATA_ASSET_PATH);
        }

        private void OnGUI()
        {
            leftSplitLine.Draw();
            leftSidebar.Draw();
            content.Draw();
            rightSpitLine.Draw();
            rightSidebar.Draw();
        }

        public void SetRightPanelState(RightPanelState rightPanelState)
        {
            this.currentRightPanelState = rightPanelState;
        }

        public void SetCurrentRandomPatternData(PatternData patternData)
        {
            this.currentRandomPatternData = patternData;
        }

        public void SetCurrentRandomSetData(SetData setData)
        {
            this.currentSetData = setData;
        }
    }
}