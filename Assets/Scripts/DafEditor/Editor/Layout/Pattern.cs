using DafEditor.Editor.Common;
using DafEditor.Editor.Intefraces;
using Data.Core;
using Data.Core.Segments;
using Data.Core.Segments.Content;
using UnityEditor;
using UnityEngine;
using EditorStyles = DafEditor.Editor.Common.EditorStyles;

namespace DafEditor.Editor.Layout
{
    public class Pattern : IDrawer
    {
        private const int PATTERN_WIDTH = 430;
        private const int PATTERN_HEIGHT = 45;

        private int segmentIndex;
        private int index;
        private Vector2 drag;
        private Vector2 otstup => new Vector2(0, 66 * index);
        Vector2 centerCoordinate => GameEditorWindow.instance.centerPoint.GetFirstPositionCoordinate();
        Vector2 position => new Vector2(centerCoordinate.x - PATTERN_WIDTH / 2 + otstup.x + drag.x, centerCoordinate.y - PATTERN_HEIGHT / 2 + otstup.y + drag.y);
        Rect rect => new Rect(position, new Vector2(PATTERN_WIDTH, PATTERN_HEIGHT));
        
        public PatternData patternData;

        public Pattern(PatternData patternData, int index)
        {
            this.patternData = patternData;
            this.index = index;
        }
        
        private Color GetSegmentColor(SegmentType segmentType)
        {
            switch (segmentType)
            {
                case SegmentType.Ground:
                    return new Color();
                case SegmentType.Hole:
                    return new Color();
                case SegmentType.Let:
                    return new Color();
            }

            return default;
        }

        public void Draw()
        {
            ProcessEvents(Event.current);
            
            GUILayout.BeginArea(rect, EditorStyles.PatternStyle());
            
            GUILayout.BeginVertical();
            
            DrawBonusLine();
            DrawSegmentLine();
            GUILayout.EndVertical();

            GUILayout.EndArea();
        }

        private void DrawSegmentLine()
        {
            var rect = new Rect(new Vector2(0, PATTERN_HEIGHT / 2), new Vector2(PATTERN_WIDTH, PATTERN_HEIGHT / 2));
            
            GUILayout.BeginArea(rect, EditorStyles.PatternSegmentStyle());
            
            GUILayout.BeginHorizontal();
            
            for (var i = 0; i < 12; i++)
            {
                switch (patternData.segmentsData[i].segmentType)
                {
                    case SegmentType.Ground:
                        if (GUILayout.Button("G", GUILayout.Width(30)))
                            patternData.segmentsData[i].segmentType = SegmentType.Hole;
                        break;
                    case SegmentType.Hole:
                        if (GUILayout.Button("H", GUILayout.Width(30)))
                            patternData.segmentsData[i].segmentType = SegmentType.Let;
                        break;
                    case SegmentType.Let:
                        if (GUILayout.Button("L", GUILayout.Width(30)))
                            patternData.segmentsData[i].segmentType = SegmentType.Ground;
                        break;
                }
            }

            if (GUILayout.Button(patternData.isRandom ? "R" : ""))
            {
                if (Event.current.button == 0)
                {
                    patternData.isRandom = !patternData.isRandom;
                    if (!patternData.isRandom)
                    {
                        GameEditorWindow.instance.SetRightPanelState(RightPanelState.Empty);
                        GameEditorWindow.instance.SetCurrentRandomPatternData(null);
                        GameEditorWindow.instance.SetCurrentRandomSetData(null);
                        patternData.isRandom = false;
                    }
                    else
                    {
                        GameEditorWindow.instance.SetRightPanelState(RightPanelState.PlatformRandomSettings);
                        GameEditorWindow.instance.SetCurrentRandomPatternData(patternData);
                        GameEditorWindow.instance.SetCurrentRandomSetData(null);
                        patternData.isRandom = true;
                    }
                }
                if (Event.current.button == 2)
                {
                    if (patternData.isRandom)
                    {
                        GameEditorWindow.instance.SetRightPanelState(RightPanelState.PlatformRandomSettings);
                        GameEditorWindow.instance.SetCurrentRandomPatternData(patternData);
                        GameEditorWindow.instance.SetCurrentRandomSetData(null);
                    }
                    else
                    {
                        GameEditorWindow.instance.SetCurrentRandomPatternData(null);
                        GameEditorWindow.instance.SetCurrentRandomSetData(null);
                        GameEditorWindow.instance.SetRightPanelState(RightPanelState.Empty);
                    }
                }
            }
            
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        
        void AddMenuItemForColor(GenericMenu menu, string menuPath, SegmentContent content)
        {
            menu.AddItem(new GUIContent(menuPath), false, OnMenuItemSelected, content);
        }
        
        void OnMenuItemSelected(object segmentContent)
        {
            patternData.segmentsData[segmentIndex].segmentContent = (SegmentContent) segmentContent;
        }
        private void DrawBonusLine()
        {
            var rect = new Rect(Vector2.zero, new Vector2(PATTERN_WIDTH, PATTERN_HEIGHT / 2));
            
            GUILayout.BeginArea(rect, EditorStyles.PatternSegmentContentStyle());
            
            GUILayout.BeginHorizontal();

            for (var i = 0; i < 12; i++)
            {
                switch (patternData.segmentsData[i].segmentContent)
                {
                    case SegmentContent.Acceleration:
                        DrawSegmentContentButton(i, "AC");
                        break;
                    case SegmentContent.Coin:
                        DrawSegmentContentButton(i, "CO");
                        break;
                    case SegmentContent.Crystal:
                        DrawSegmentContentButton(i, "CR");
                        break;
                    case SegmentContent.Magnet:
                        DrawSegmentContentButton(i, "MA");
                        break;
                    case SegmentContent.Multiplier:
                        DrawSegmentContentButton(i, "MU");
                        break;
                    case SegmentContent.None:
                        DrawSegmentContentButton(i, "-");
                        break;
                    case SegmentContent.Shield:
                        DrawSegmentContentButton(i, "SH");
                        break;
                    case SegmentContent.Key:
                        DrawSegmentContentButton(i, "KE");
                        break;
                    case SegmentContent.Random:
                        DrawSegmentContentButton(i, "R");
                        break;
                }
            }

            GUILayout.Button("X");
            
            GUILayout.EndHorizontal();
            
            GUILayout.EndArea();
        }

        private void DrawSegmentContentButton(int i, string content)
        {
            if (GUILayout.Button(content, GUILayout.Width(30)))
            {
                if (Event.current.button == 0) {
                    segmentIndex = i;
                
                    var menu = new GenericMenu();

                    AddMenuItemForColor(menu, "Bonus/Acceleration", SegmentContent.Acceleration);
                    AddMenuItemForColor(menu, "Bonus/Multiplier", SegmentContent.Multiplier);
                    AddMenuItemForColor(menu, "Bonus/Shield", SegmentContent.Shield);
                    AddMenuItemForColor(menu, "Bonus/Magnet", SegmentContent.Magnet);
                    AddMenuItemForColor(menu, "Bonus/Key", SegmentContent.Key);
                
                    menu.AddSeparator("");

                    AddMenuItemForColor(menu, "Currencies/Coin", SegmentContent.Coin);
                    AddMenuItemForColor(menu, "Currencies/Crystal", SegmentContent.Crystal);
                
                    menu.AddSeparator("");
                    AddMenuItemForColor(menu, "Random", SegmentContent.Random);
                
                    menu.AddSeparator("");
                    AddMenuItemForColor(menu, "None", SegmentContent.None);
                
                    menu.ShowAsContext();
                } else if (Event.current.button == 2) {
                    if (patternData.segmentsData[segmentIndex].segmentContent == SegmentContent.Random)
                    {
                        GameEditorWindow.instance.currentRightPanelState = RightPanelState.BonusRandomSettings;
                    }
                    else
                    {
                        GameEditorWindow.instance.currentRightPanelState = RightPanelState.Empty;
                    }
                }
            }
        }

        private void ProcessEvents(Event _e)
        {
            switch (_e.type)
            {
                case EventType.MouseDrag:
                    if (_e.button == 1) OnDrag(_e.delta);
                    break;
            }
        }

        private void OnDrag(Vector2 _delta)
        {
            drag += _delta;
            GUI.changed = true;
        }
    }
}