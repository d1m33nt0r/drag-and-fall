using UnityEngine;

namespace DafEditor.Editor.Common
{
    public class CenterPoint
    {
        private SplitLine m_splitLine;

        public Vector2 GetCenterCoordinate()
        {
            if (m_splitLine == null)
                m_splitLine = GameEditorWindow.instance.m_splitLine;

            var flowWidth = GameEditorWindow.instance.position.width - m_splitLine.xSplitCoordinate;

            var centerX = flowWidth / 2;
            var centerY = GameEditorWindow.instance.position.height / 2;

            return new Vector2(centerX, centerY);
        }
        
        public Vector2 GetFirstPositionCoordinate()
        {
            if (m_splitLine == null)
                m_splitLine = GameEditorWindow.instance.m_splitLine;

            var flowWidth = GameEditorWindow.instance.position.width - m_splitLine.xSplitCoordinate;

            var centerX = flowWidth / 2;
            var centerY = 100;

            return new Vector2(centerX, centerY);
        }
    }
}