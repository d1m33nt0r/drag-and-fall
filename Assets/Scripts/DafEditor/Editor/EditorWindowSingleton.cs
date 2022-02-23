using UnityEditor;
using UnityEngine;

namespace DafEditor.Editor
{
    public abstract class EditorWindowSingleton<TSelfType> : EditorWindow
        where TSelfType : EditorWindowSingleton<TSelfType>
    {
        protected virtual string customTitle => "window title";
        private static TSelfType m_instance;

        public static TSelfType instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GetWindow<TSelfType>();
                    m_instance.titleContent = new GUIContent(m_instance.customTitle);
                }

                return m_instance;
            }
        }
    }
}