using DafEditor.Editor.Intefraces;
using DafEditor.Editor.Layout;
using UnityEditor;
using UnityEngine;

namespace DafEditor.Editor.Common
{
    public class RightSplitLine : LayoutComponent, IDrawer
    {
        private readonly Color SPLIT_LINE_COLOR = new Color(0, 0, 0, 0.4f);
        
        public void Draw()
        {
            if (!isInitialized) return;
            Handles.color = SPLIT_LINE_COLOR;
            var m_startPosition = new Vector2(gameEditorWindow.position.size.x - 250, 25);
            Handles.DrawLine(m_startPosition, new Vector2(gameEditorWindow.position.size.x - 250, gameEditorWindow.position.height), 0f);
        }
    }
}