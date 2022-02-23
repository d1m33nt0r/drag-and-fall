using DafEditor.Editor.Intefraces;
using UnityEditor;
using UnityEngine;

namespace DafEditor.Editor.Common
{
    public class TopBarSplitLine : IDrawer
    {
        public float ySplitCoordinate => m_startPostion.y;

        private const int X_SPLIT_COORDINATE = 250;
        private readonly Color SPLIT_LINE_COLOR = new Color(0, 0, 0, 0.4f);
        private Vector2 m_startPostion = new Vector2(0, 23 + 1);

        public void Draw()
        {
            Handles.color = SPLIT_LINE_COLOR;
            Handles.DrawLine(m_startPostion, new Vector2(GameEditorWindow.instance.position.width, ySplitCoordinate),
                2f);
        }
    }
}