using DafEditor.Editor.Intefraces;
using UnityEditor;
using UnityEngine;

namespace DafEditor.Editor.Common
{
    public class LeftSplitLine : IDrawer
    {
        public float xSplitCoordinate => m_startPostion.x;
        public float lineWidth => 1f;
        
        private const int X_SPLIT_COORDINATE = 250;
        private readonly Color SPLIT_LINE_COLOR = new Color(0, 0, 0, 0.4f);
        private Vector2 m_startPostion = new Vector2(X_SPLIT_COORDINATE, 25);
        
        public void Draw()
        {
            Handles.color = SPLIT_LINE_COLOR;
            Handles.DrawLine(m_startPostion, new Vector2(X_SPLIT_COORDINATE, GameEditorWindow.instance.position.height), 0f);
        }
    }
}