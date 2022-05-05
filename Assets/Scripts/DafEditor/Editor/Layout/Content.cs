using System.Collections.Generic;
using DafEditor.Editor.Common;
using DafEditor.Editor.Intefraces;
using Data.Core;
using UnityEditor;
using UnityEngine;
using EditorStyles = DafEditor.Editor.Common.EditorStyles;

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
            _zoomArea = new Rect(
                new Vector2(EditorWindowSingleton<GameEditorWindow>.instance.leftSplitLine.xSplitCoordinate, 55),
                new Vector2(
                    EditorWindowSingleton<GameEditorWindow>.instance.position.size.x - 250 - 250,
                    EditorWindowSingleton<GameEditorWindow>.instance.position.height));
            if (!isInitialized) return;
            
            DrawHeaderPanel();

            DrawGridSection();
        }

      
        private Vector2 _zoomCoordsOrigin = Vector2.zero;
        
        private float _zoom = 1.0f;
        private Rect _zoomArea;

        private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
        {
            return (screenCoords - _zoomArea.TopLeft()) / _zoom + _zoomCoordsOrigin;
        }
        private const float kZoomMin = 0.1f;
        private const float kZoomMax = 10.0f;
        private void HandleEvents()
        {
            if (Event.current.type == EventType.ScrollWheel)
            {
                Vector2 screenCoordsMousePos = Event.current.mousePosition;
                Vector2 delta = Event.current.delta;
                Vector2 zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(screenCoordsMousePos);
                float zoomDelta = -delta.y / 150.0f;
                float oldZoom = _zoom;
                _zoom += zoomDelta;
                _zoom = Mathf.Clamp(_zoom, kZoomMin, kZoomMax);
                _zoomCoordsOrigin += (zoomCoordsMousePos - _zoomCoordsOrigin) - (oldZoom / _zoom) * (zoomCoordsMousePos - _zoomCoordsOrigin);
 
                Event.current.Use();
            }
        }
        
        private void DrawGridSection()
        {
            HandleEvents();
            var contentRect = new Rect(
                new Vector2(-_zoomCoordsOrigin.x, -_zoomCoordsOrigin.y),
                new Vector2(gameEditorWindow.position.size.x - gameEditorWindow.leftSplitLine.xSplitCoordinate - 250, gameEditorWindow.position.height));
          
            //gridDrawer.Draw();
            EditorZoomArea.Begin(_zoom, _zoomArea);
            //GUILayout.BeginArea(_zoomArea, EditorStyles.LeftSidebarStyle());
            DrawPatternEditor();
            //GUILayout.EndArea();
            EditorZoomArea.End();
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

            /*if (GUILayout.Button("Add new pattern"))
            {
                PatternDatas.Add(new PatternData(12));
                if (PatternDatas.Count > 1) PatternDatas[PatternDatas.Count - 2].isLast = false;
                PatternDatas[PatternDatas.Count - 1].isLast = true;
                SetPatterns(PatternDatas, name);
                EditorUtility.SetDirty(gameEditorWindow.currentLevelData);
            }*/

            GUILayout.EndArea();
        }
    }
}