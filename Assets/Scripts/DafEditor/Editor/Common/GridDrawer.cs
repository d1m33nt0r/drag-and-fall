using DafEditor.Editor.Intefraces;
using UnityEditor;
using UnityEngine;

namespace DafEditor.Editor.Common
{
    public class GridDrawer : IDrawer
    {
        private Vector2 m_offset;
        private Vector2 m_drag;
        
        public void Draw()
        {
            DrawGrid(20, 0.2f, new Color(0.1f, 0.1f, 0.1f, 0.05f));
            DrawGrid(100, 0.4f, new Color(0.1f, 0.1f, 0.1f, 0.1f));
        }
        
        private void DrawGrid(float _gridSpacing, float _gridOpacity, Color _gridColor)
        {
            var widthDivs = Mathf.CeilToInt(GameEditorWindow.instance.position.width / _gridSpacing);
            var heightDivs = Mathf.CeilToInt(GameEditorWindow.instance.position.height / _gridSpacing);
 
            Handles.BeginGUI();
            Handles.color = new Color(_gridColor.r, _gridColor.g, _gridColor.b, _gridOpacity);
 
            m_offset += m_drag * 0.5f;
            var newOffset = new Vector2(m_offset.x % _gridSpacing, m_offset.y % _gridSpacing);
 
            for (var i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector2(_gridSpacing * i, -_gridSpacing) + newOffset, 
                    new Vector2(_gridSpacing * i, GameEditorWindow.instance.position.height) + newOffset);
            }
 
            for (var j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector2(0, _gridSpacing * j) + newOffset, 
                    new Vector2(GameEditorWindow.instance.position.width, _gridSpacing * j) + newOffset);
            }
 
            Handles.color = Color.white;
            Handles.EndGUI();
        }
    }
}