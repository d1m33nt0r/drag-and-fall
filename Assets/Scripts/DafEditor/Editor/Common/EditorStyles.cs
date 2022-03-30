using UnityEngine;

namespace DafEditor.Editor.Common
{
    public static class EditorStyles
    {
        public static GUIStyle LeftSidebarStyle()
        {
            var style = new GUIStyle();
            
            var texture = new Texture2D(1,1);
            texture.SetPixel(1,1, new Color(0.2f,0.2f,0.2f,1));
            texture.Apply();

            style.padding = new RectOffset(0, 0, 2, 0);
            style.normal.background = texture;

            return style;
        }
        
        public static GUIStyle PatternStyle()
        {
            var style = new GUIStyle();
            
            var texture = new Texture2D(1,1);
            texture.SetPixel(1,1, new Color(0.0f,0.0f,0.2f,0.7f));
            texture.Apply();
            
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.background = texture;

            return style;
        }
        
        public static GUIStyle PatternSegmentContentStyle()
        {
            var style = new GUIStyle();
            
            var texture = new Texture2D(1,1);
            texture.SetPixel(1,1, new Color(0.2f,0.2f,0.0f,0.7f));
            texture.Apply();
            
            style.normal.background = texture;

            return style;
        }
        
        public static GUIStyle PatternSegmentStyle()
        {
            var style = new GUIStyle();
            
            var texture = new Texture2D(1,1);
            texture.SetPixel(1,1, new Color(0.0f,0.2f,0.2f,0.7f));
            texture.Apply();
            
            style.normal.background = texture;

            return style;
        }

        public static GUIStyle DarkButtonStyle(int _height)
        {
            var style = new GUIStyle();
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(0.17f, 0.17f, 0.17f, 0.9f));
            texture.Apply();
            var activeTexture = new Texture2D(1, 1);
            activeTexture.SetPixel(1, 1, new Color(0.75f, 0.4f, 0.4f, 0.1f));
            activeTexture.Apply();
            style.fixedHeight = _height;
            style.normal.background = texture;
            style.active.background = activeTexture;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            style.margin = new RectOffset(0, 0, 2, 2);

            return style;
        }
        
        public static GUIStyle RedButtonStyle(int _height)
        {
            var style = new GUIStyle();
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(0.5f, 0.0f, 0.1f, 0.2f));
            texture.Apply();
            var activeTexture = new Texture2D(1, 1);
            activeTexture.SetPixel(1, 1, new Color(0.75f, 0.4f, 0.4f, 0.1f));
            activeTexture.Apply();
            style.fixedHeight = _height;
            style.normal.background = texture;
            style.active.background = activeTexture;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            style.margin = new RectOffset(0, 0, 2, 2);

            return style;
        }

        public static GUIStyle BlueButtonStyle(int _height)
        {
            var style = new GUIStyle();
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(0.0f, 0.0f, 0.5f, 0.2f));
            texture.Apply();
            var activeTexture = new Texture2D(1, 1);
            activeTexture.SetPixel(1, 1, new Color(0.75f, 0.4f, 0.4f, 0.1f));
            activeTexture.Apply();
            style.fixedHeight = _height;
            style.normal.background = texture;
            style.active.background = activeTexture;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            style.margin = new RectOffset(0, 0, 2, 2);

            return style;
        }
        
        public static GUIStyle HeaderOfBlockInLeftSideBar()
        {
            var style = new GUIStyle();
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(0.25f, 0.1f, 0.1f, 0.5f));
            texture.Apply();
            style.fixedHeight = 24;
            style.normal.background = texture;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;

            return style;
        }
        
        public static GUIStyle HeaderOfBlockInRightSideBar()
        {
            var style = new GUIStyle();
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(0.0f, 0.0f, 0.1f, 0.3f));
            texture.Apply();
            style.fixedHeight = 24;
            style.normal.background = texture;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;

            return style;
        }
        
        public static GUIStyle HeaderOfBlockInRightSideBar2()
        {
            var style = new GUIStyle();
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(0.0f, 0.0f, 0.1f, 0.1f));
            texture.Apply();
            style.fixedHeight = 24;
            style.normal.background = texture;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;

            return style;
        }
        
        public static GUIStyle IdLabelStyle(int _height, int _width)
        {
            var style = new GUIStyle();
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(0.15f, 0.1f, 0.25f, 0.25f));
            texture.Apply();
            style.fixedHeight = _height;
            style.normal.background = texture;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            style.fixedWidth = _width;

            return style;
        }
        
        public static GUIStyle TopBarBackgroundStyle()
        {
            var style = new GUIStyle();

            var texture = new Texture2D(1,1);
            texture.SetPixel(1,1, new Color(0.2f,0.2f,0.2f,1));
            texture.Apply();
            
            style.normal.background = texture;

            return style;
        }

        public static GUIStyle SetButtonStyle()
        {
            var style = new GUIStyle();
            
            var texture = new Texture2D(1,1);
            texture.SetPixel(1,1, new Color(0f,0f,0f,0.4f));
            texture.Apply();

            style.normal.background = texture;

            return style;
        }
    }
}