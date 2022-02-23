using UnityEngine;

namespace PatternManager.Editor
{
    public class Toolbar
    {
        private string[] m_options = { "Infinity", "Levels"};
        private int currentOption = 0;
        
        public int Draw()
        {
            currentOption = GUILayout.Toolbar(currentOption, m_options);
            return currentOption;
        }
    }
}