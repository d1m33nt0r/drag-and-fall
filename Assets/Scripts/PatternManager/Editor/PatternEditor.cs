using UnityEditor;
using UnityEngine;
using EditorStyles = PatternManager.Editor.Common.EditorStyles;

namespace PatternManager.Editor
{
    [CustomEditor(typeof(PatternStorage))]
    public class PatternEditor : UnityEditor.Editor
    {
        private PatternStorage m_storage;
        private Toolbar m_toolbar = new Toolbar();
        private int patternsAmount;

        private bool[] infinityFoldedSets;
        
        public void OnEnable()
        {

            m_storage = (PatternStorage)target;
            infinityFoldedSets = new bool[m_storage.infinitySets.Count];
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            switch (m_toolbar.Draw())
            {
                case 0:
                    
                    var i = 0;
                    foreach (var infinitySet in m_storage.infinitySets)
                    {
                        DrawSet(infinitySet, i);
                        i++;
                    }
                    
                    patternsAmount = EditorGUILayout.IntField("Patterns amount", patternsAmount);

                    if (GUILayout.Button("Add new set"))
                    {
                        m_storage.infinitySets.Add(new SetData(patternsAmount));
                        infinityFoldedSets = new bool[m_storage.infinitySets.Count];
                    }
                        
                    
                    break;
                case 1:
                    
                    break;
            }
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);        
            }
        }

        private void DrawSet(SetData setData, int _index)
        {
            if (GUILayout.Button("Set " + _index, EditorStyles.SetButtonStyle()))
                infinityFoldedSets[_index] = !infinityFoldedSets[_index];
            
            if (infinityFoldedSets[_index])
            {
                foreach (var pattern in setData.patterns)
                {
                    GUILayout.BeginHorizontal("box");
                    
                    foreach (var segment in pattern.segmentsData)
                    {
                        var style = new PlatformButton(segment, new GUIStyle());
                        if (GUILayout.Button(style.value, style.style))
                            SwitchSegmentType(segment);
                    }

                    if (GUILayout.Button("✎", new GUIStyle()))
                    {
                        var menu = new GenericMenu();
                        
                        menu.AddItem(new GUIContent("Move up"), false, () =>
                        {
                            if (pattern.index == 0) return;
                            var prevPattern = setData.patterns[pattern.index - 1];
                            setData.patterns[pattern.index - 1] = pattern;
                            pattern.index -= 1;
                            prevPattern.index += 1;
                            setData.patterns[prevPattern.index] = prevPattern;
                        });
                        
                        menu.AddItem(new GUIContent("Move down"), false, () =>
                        {
                            if (pattern.index + 1 == setData.patterns.Length) return;
                            var nextPattern = setData.patterns[pattern.index + 1];
                            setData.patterns[pattern.index + 1] = pattern;
                            pattern.index += 1;
                            nextPattern.index -= 1;
                            setData.patterns[nextPattern.index] = nextPattern;
                        });
                        
                        menu.AddItem(new GUIContent("Remove"), false, () =>
                        {
                            
                        });
                        
                        menu.AddItem(new GUIContent("Make random"), false, () =>
                        {
                            
                        });
                        
                        menu.ShowAsContext();
                    }
                
                    GUILayout.EndHorizontal();
                }
            }
        }

        private static void SwitchSegmentType(SegmentData segmentData)
        {
            switch (segmentData.segmentType)
            {
                case SegmentType.Ground:
                    segmentData.segmentType = SegmentType.Hole;
                    break;
                case SegmentType.Hole:
                    segmentData.segmentType = SegmentType.Let;
                    break;
                case SegmentType.Let:
                    segmentData.segmentType = SegmentType.Ground;
                    break;
            }
        }
    }

    class PlatformButton
    {
        public string value;
        public GUIStyle style;

        public PlatformButton(SegmentData segmentData, GUIStyle m_style)
        {
            switch (segmentData.segmentType)
            {
                case SegmentType.Ground:
                    value = "G";
                    style = m_style;
                    break;
                case SegmentType.Hole:
                    value = "H";
                    style = m_style;
                    break;
                case SegmentType.Let:
                    value = "L";
                    style = m_style;
                    break;
            }
        }
    }
}