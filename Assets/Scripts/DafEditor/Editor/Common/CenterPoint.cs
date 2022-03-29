using UnityEngine;

namespace DafEditor.Editor.Common
{
    public class CenterPoint
    {
        private LeftSplitLine mLeftSplitLine;

        public Vector2 GetCenterCoordinate()
        {
            if (mLeftSplitLine == null)
                mLeftSplitLine = GameEditorWindow.instance.leftSplitLine;

            var flowWidth = GameEditorWindow.instance.position.width - mLeftSplitLine.xSplitCoordinate;

            var centerX = flowWidth / 2;
            var centerY = GameEditorWindow.instance.position.height / 2;

            return new Vector2(centerX, centerY);
        }
        
        public Vector2 GetFirstPositionCoordinate()
        {
            if (mLeftSplitLine == null)
                mLeftSplitLine = GameEditorWindow.instance.leftSplitLine;

            var flowWidth = GameEditorWindow.instance.position.width - mLeftSplitLine.xSplitCoordinate - 249;

            var centerX = flowWidth / 2;
            var centerY = 100;

            return new Vector2(centerX, centerY);
        }
    }
}