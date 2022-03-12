using System.Collections.Generic;
using DafEditor.Editor.Common;
using DafEditor.Editor.Intefraces;
using Data.Core;
using UnityEngine;

namespace DafEditor.Editor.Layout
{
    public class Content : IDrawer
    {
        private GridDrawer gridDrawer = new GridDrawer();
        private string name;
        private List<PatternData> PatternDatas;
        public List<Pattern> patterns = new List<Pattern>();

        public void SetPatterns(List<PatternData> _patternDatas, string name)
        {
            patterns.Clear();
            this.name = name;
            PatternDatas = _patternDatas;
            for (var i = 0; i < _patternDatas.Count; i++)
                patterns.Add(new Pattern(_patternDatas[i].segmentsData, i));
        }
        
        public void Draw()
        {
            GUILayout.BeginArea(new Rect(
                new Vector2(GameEditorWindow.instance.m_splitLine.xSplitCoordinate,
                    GameEditorWindow.instance.m_topBar.TOP_BAR_HEIGHT),
                new Vector2(
                    GameEditorWindow.instance.position.width - GameEditorWindow.instance.m_splitLine.xSplitCoordinate,
                    GameEditorWindow.instance.position.height)), EditorStyles.LeftSidebarStyle());

            gridDrawer.Draw();

            GUILayout.Label(name, EditorStyles.HeaderOfBlockInLeftSideBar());
            if (GUILayout.Button("Add new pattern"))
            {
                PatternDatas.Add(new PatternData(12));
                SetPatterns(PatternDatas, name);
            }

            for (var i = 0; i < patterns.Count; i++)
                patterns[i].Draw();

            GUILayout.EndArea();
        }
    }
}