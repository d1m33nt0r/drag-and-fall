using System.Collections.Generic;
using DafEditor.Editor.Common;
using DafEditor.Editor.Intefraces;
using Data.Core;
using UnityEngine;

namespace DafEditor.Editor.Layout
{
    public class Content : LayoutComponent, IDrawer
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
                patterns.Add(new Pattern(_patternDatas[i], i));
        }
        
        public void Draw()
        {
            if (!isInitialized) return;
            
            DrawHeaderPanel();

            DrawGridSection();
        }

        private void DrawGridSection()
        {
            GUILayout.BeginArea(new Rect(
                new Vector2(gameEditorWindow.leftSplitLine.xSplitCoordinate, 55),
                new Vector2(
                    gameEditorWindow.position.size.x - gameEditorWindow.leftSplitLine.xSplitCoordinate - 250,
                    gameEditorWindow.position.height)), EditorStyles.LeftSidebarStyle());

            gridDrawer.Draw();

            DrawPatternEditor();

            GUILayout.EndArea();
        }

        private void DrawPatternEditor()
        {
            for (var i = 0; i < patterns.Count; i++)
                patterns[i].Draw();
        }

        private void DrawHeaderPanel()
        {
            GUILayout.BeginArea(new Rect(
                new Vector2(gameEditorWindow.leftSplitLine.xSplitCoordinate, 0),
                new Vector2(
                    gameEditorWindow.position.size.x - gameEditorWindow.leftSplitLine.xSplitCoordinate - 250,
                    55)), EditorStyles.LeftSidebarStyle());

            GUILayout.Label(name, EditorStyles.HeaderOfBlockInLeftSideBar());

            if (GUILayout.Button("Add new pattern"))
            {
                PatternDatas.Add(new PatternData(12));
                if (PatternDatas.Count > 1) PatternDatas[PatternDatas.Count - 2].isLast = false;
                PatternDatas[PatternDatas.Count - 1].isLast = true;
                SetPatterns(PatternDatas, name);
            }

            GUILayout.EndArea();
        }
    }
}