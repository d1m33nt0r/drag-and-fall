using System.Linq;
using DafEditor.Editor.Common;
using DafEditor.Editor.Intefraces;
using Data;
using UnityEditor;
using UnityEngine;

namespace DafEditor.Editor.Layout
{
    public class RightSidebar : LayoutComponent, IDrawer
    {
        private Vector2 scrollPos;
        
        public void Draw()
        {
            if (!isInitialized) return;
            
            GUILayout.BeginArea(new Rect(new Vector2(gameEditorWindow.position.size.x - 300, 0),
                new Vector2(300,
                    gameEditorWindow.position.height)), Common.EditorStyles.LeftSidebarStyle());

            GUILayout.Label("Random settings", Common.EditorStyles.HeaderOfBlockInLeftSideBar());
            scrollPos =
                EditorGUILayout.BeginScrollView(scrollPos, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.Width(300), GUILayout.Height(gameEditorWindow.position.height - 30));
            switch (gameEditorWindow.currentRightPanelState)
            {
                case RightPanelState.BonusRandomSettings:
                    GUILayout.Label("Bonus random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar());
                    
                    break;
                case RightPanelState.PlatformRandomSettings:
                    GUILayout.Label("Platform random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar());
                    GUILayout.BeginVertical();
                    
                    EditorGUI.BeginChangeCheck();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Min hole amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentRandomPatternData.minHoleAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentRandomPatternData.minHoleAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Max hole amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentRandomPatternData.maxHoleAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentRandomPatternData.maxHoleAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Min let amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentRandomPatternData.minLetAmount = 
                        EditorGUILayout.IntSlider( gameEditorWindow.currentRandomPatternData.minLetAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginHorizontal(new GUIStyle{fixedWidth = 100});
                    GUILayout.Label("Max hole amount");
                    GUILayout.EndHorizontal();
                    gameEditorWindow.currentRandomPatternData.maxLetAmount = 
                        EditorGUILayout.IntSlider(gameEditorWindow.currentRandomPatternData.maxLetAmount, 0, 12);
                    GUILayout.EndHorizontal();
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorUtility.SetDirty(gameEditorWindow.infinityData.patternSets[0]);
                    }
                    GUILayout.EndVertical();
                    break;
                case RightPanelState.SegmentRandomSettings:
                    GUILayout.Label("Segment random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar());
                    break;
                case RightPanelState.SetRandomSettings:
                    
                    GUILayout.Label("Set random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar2());
                    
                    EditorGUI.BeginChangeCheck();
                    
                    DrawSetRandomSettings();

                    GUILayout.Label("Power ups random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar2());
                    
                    DrawShieldRandomSettings();

                    DrawAccelerationRandomSettings();

                    DrawMultiplierRandomSettings();

                    DrawMagnetRandomSettings();

                    DrawKeyRandomSettings();

                    GUILayout.Label("Currencies random settings", Common.EditorStyles.HeaderOfBlockInRightSideBar2());

                    if (gameEditorWindow.currentSetData.currencyRandomSettings == null)
                    {
                        GUILayout.Label("Add first currency profile!");
                    }
                    else
                    {
                        for (var i = 0; i < gameEditorWindow.currentSetData.currencyRandomSettings.Length; i++)
                        {
                            DrawCoinRandomSettings(i);
                        } 
                    }
                    
                    if (GUILayout.Button("Add currency profile"))
                    {
                        if (gameEditorWindow.currentSetData.currencyRandomSettings == null)
                        {
                            gameEditorWindow.currentSetData.currencyRandomSettings = new CurrencyRandomSettings[1];
                        }
                        else
                        {
                            var list = gameEditorWindow.currentSetData.currencyRandomSettings.ToList();
                            list.Add(new CurrencyRandomSettings());
                            gameEditorWindow.currentSetData.currencyRandomSettings = list.ToArray(); 
                        }
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorUtility.SetDirty(gameEditorWindow.currentSetData);
                    }
                    
                    break;
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        private void DrawSetRandomSettings()
        {
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Min platforms");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.minPlatformsCount =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.minPlatformsCount, 0, 500);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Max platforms");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.maxPlatformsCount =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.maxPlatformsCount, 0, 500);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Min hole amount");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.minHoleAmount =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.minHoleAmount, 0, 12);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Max hole amount");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.maxHoleAmount =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.maxHoleAmount, 0, 12);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Min let amount");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.minLetAmount =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.minLetAmount, 0, 12);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Max let amount");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.maxLetAmount =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.maxLetAmount, 0, 12);
            GUILayout.EndHorizontal();
        }

        private void DrawCoinRandomSettings(int index)
        {
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Currency type");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.currencyRandomSettings[index].currencyType =
                (CurrencyType)EditorGUILayout.EnumPopup(gameEditorWindow.currentSetData.currencyRandomSettings[index].currencyType);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Spawn chance");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.currencyRandomSettings[index].spawnChance =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.currencyRandomSettings[index].spawnChance, 0, 100);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginVertical("box");
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("");
            GUILayout.EndHorizontal();
            GUILayout.Label("L");
            GUILayout.Label("G");
            GUILayout.Label("H");
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("Spawn on");
            GUILayout.EndHorizontal();
            
            gameEditorWindow.currentSetData.currencyRandomSettings[index].spawnOnLet =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.currencyRandomSettings[index].spawnOnLet);
            gameEditorWindow.currentSetData.currencyRandomSettings[index].spawnOnGround =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.currencyRandomSettings[index].spawnOnGround);
            gameEditorWindow.currentSetData.currencyRandomSettings[index].spawnOnHole =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.currencyRandomSettings[index].spawnOnHole);
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("Range start");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.currencyRandomSettings[index].rangeStart =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.currencyRandomSettings[index].rangeStart, 0, 500);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("Range end");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.currencyRandomSettings[index].rangeEnd =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.currencyRandomSettings[index].rangeEnd, 0, 500);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Accuracy level");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.currencyRandomSettings[index].accuracyLevel =
                (AccuracyLevel) EditorGUILayout.EnumPopup(gameEditorWindow.currentSetData.currencyRandomSettings[index].accuracyLevel);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Remove profile"))
            {
                var list = gameEditorWindow.currentSetData.currencyRandomSettings.ToList();
                list.Remove(gameEditorWindow.currentSetData.currencyRandomSettings[index]);
                gameEditorWindow.currentSetData.currencyRandomSettings = list.ToArray();
            }
            
            GUILayout.EndVertical();
        }

        private void DrawKeyRandomSettings()
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Key");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.attemptsOfKeyInstantiate =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.attemptsOfKeyInstantiate, 0, 5);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Spawn chance");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.spawnKeyChance = 
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.spawnKeyChance, 0, 100);
            //GUILayout.Label("%", new GUIStyle{fixedWidth = 10, alignment = TextAnchor.MiddleCenter, fontSize = 16, border = new RectOffset(5, 0, 0, 0)});
            GUILayout.EndHorizontal();
            
            if (gameEditorWindow.currentSetData.keyPositions.Length !=
                gameEditorWindow.currentSetData.attemptsOfKeyInstantiate)
            {
                if (gameEditorWindow.currentSetData.keyPositions.Length >
                    gameEditorWindow.currentSetData.attemptsOfKeyInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfKeyInstantiate];
                    for (var i = 0; i < newPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.keyPositions[i];
                    gameEditorWindow.currentSetData.keyPositions = newPositions;
                }
                else if (gameEditorWindow.currentSetData.keyPositions.Length <
                         gameEditorWindow.currentSetData.attemptsOfKeyInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfKeyInstantiate];
                    for (var i = 0; i < gameEditorWindow.currentSetData.keyPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.keyPositions[i];

                    gameEditorWindow.currentSetData.keyPositions = newPositions;
                }
            }

            GUILayout.BeginVertical("box");
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("");
            GUILayout.EndHorizontal();
            GUILayout.Label("L");
            GUILayout.Label("G");
            GUILayout.Label("H");
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("Spawn on");
            GUILayout.EndHorizontal();
            
            gameEditorWindow.currentSetData.spawnKeyOnLet =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnKeyOnLet);
            gameEditorWindow.currentSetData.spawnKeyOnGround =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnKeyOnGround);
            gameEditorWindow.currentSetData.spawnKeyOnHole =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnKeyOnHole);
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            for (var i = 0; i < gameEditorWindow.currentSetData.attemptsOfKeyInstantiate; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
                GUILayout.Label("Platform index");
                GUILayout.EndHorizontal();
                gameEditorWindow.currentSetData.keyPositions[i] =
                    EditorGUILayout.IntField(gameEditorWindow.currentSetData.keyPositions[i]);
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }

        private void DrawMagnetRandomSettings()
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Manget");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.attemptsOfMagnetInstantiate =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.attemptsOfMagnetInstantiate, 0, 5);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Spawn chance");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.spawnMagnetChance = 
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.spawnMagnetChance, 0, 100);
            //GUILayout.Label("%", new GUIStyle{fixedWidth = 10, alignment = TextAnchor.MiddleCenter, fontSize = 16, border = new RectOffset(5, 0, 0, 0)});
            GUILayout.EndHorizontal();
            
            if (gameEditorWindow.currentSetData.magnetPositions.Length !=
                gameEditorWindow.currentSetData.attemptsOfMagnetInstantiate)
            {
                if (gameEditorWindow.currentSetData.magnetPositions.Length >
                    gameEditorWindow.currentSetData.attemptsOfMagnetInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfMagnetInstantiate];
                    for (var i = 0; i < newPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.magnetPositions[i];
                    gameEditorWindow.currentSetData.magnetPositions = newPositions;
                }
                else if (gameEditorWindow.currentSetData.magnetPositions.Length <
                         gameEditorWindow.currentSetData.attemptsOfMagnetInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfMagnetInstantiate];
                    for (var i = 0; i < gameEditorWindow.currentSetData.magnetPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.magnetPositions[i];

                    gameEditorWindow.currentSetData.magnetPositions = newPositions;
                }
            }
            
            GUILayout.BeginVertical("box");
            
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("");
            GUILayout.EndHorizontal();
            GUILayout.Label("L");
            GUILayout.Label("G");
            GUILayout.Label("H");
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("Spawn on");
            GUILayout.EndHorizontal();
            
            gameEditorWindow.currentSetData.spawnMagnetOnLet =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnMagnetOnLet);
            gameEditorWindow.currentSetData.spawnMagnetOnGround =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnMagnetOnGround);
            gameEditorWindow.currentSetData.spawnMagnetOnHole =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnMagnetOnHole);
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            
            for (var i = 0; i < gameEditorWindow.currentSetData.attemptsOfMagnetInstantiate; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
                GUILayout.Label("Platform index");
                GUILayout.EndHorizontal();
                gameEditorWindow.currentSetData.magnetPositions[i] =
                    EditorGUILayout.IntField(gameEditorWindow.currentSetData.magnetPositions[i]);
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }

        private void DrawMultiplierRandomSettings()
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Multiplier");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.attemptsOfMultiplierInstantiate =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.attemptsOfMultiplierInstantiate, 0, 5);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Spawn chance");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.spawnMultiplierChance = 
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.spawnMultiplierChance, 0, 100);
            //GUILayout.Label("%", new GUIStyle{fixedWidth = 10, alignment = TextAnchor.MiddleCenter, fontSize = 16, border = new RectOffset(5, 0, 0, 0)});
            GUILayout.EndHorizontal();
            
            if (gameEditorWindow.currentSetData.multiplierPositions.Length !=
                gameEditorWindow.currentSetData.attemptsOfMultiplierInstantiate)
            {
                if (gameEditorWindow.currentSetData.multiplierPositions.Length >
                    gameEditorWindow.currentSetData.attemptsOfMultiplierInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfMultiplierInstantiate];
                    for (var i = 0; i < newPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.multiplierPositions[i];
                    gameEditorWindow.currentSetData.multiplierPositions = newPositions;
                }
                else if (gameEditorWindow.currentSetData.multiplierPositions.Length <
                         gameEditorWindow.currentSetData.attemptsOfMultiplierInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfMultiplierInstantiate];
                    for (var i = 0; i < gameEditorWindow.currentSetData.multiplierPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.multiplierPositions[i];

                    gameEditorWindow.currentSetData.multiplierPositions = newPositions;
                }
            }

            GUILayout.BeginVertical("box");
            
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("");
            GUILayout.EndHorizontal();
            GUILayout.Label("L");
            GUILayout.Label("G");
            GUILayout.Label("H");
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("Spawn on");
            GUILayout.EndHorizontal();
            
            gameEditorWindow.currentSetData.spawnMultiplierOnLet =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnMultiplierOnLet);
            gameEditorWindow.currentSetData.spawnMultiplierOnGround =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnMultiplierOnGround);
            gameEditorWindow.currentSetData.spawnMultiplierOnHole =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnMultiplierOnHole);
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            for (var i = 0; i < gameEditorWindow.currentSetData.attemptsOfMultiplierInstantiate; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
                GUILayout.Label("Platform index");
                GUILayout.EndHorizontal();
                gameEditorWindow.currentSetData.multiplierPositions[i] =
                    EditorGUILayout.IntField(gameEditorWindow.currentSetData.multiplierPositions[i]);
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }

        private void DrawAccelerationRandomSettings()
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Acceleration");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.attemptsOfAccelerationInstantiate =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.attemptsOfAccelerationInstantiate, 0, 5);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Spawn chance");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.spawnAccelerationChance = 
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.spawnAccelerationChance, 0, 100);
            //GUILayout.Label("%", new GUIStyle{fixedWidth = 10, alignment = TextAnchor.MiddleCenter, fontSize = 16, border = new RectOffset(5, 0, 0, 0)});
            GUILayout.EndHorizontal();
            
            if (gameEditorWindow.currentSetData.accelerationPositions.Length !=
                gameEditorWindow.currentSetData.attemptsOfAccelerationInstantiate)
            {
                if (gameEditorWindow.currentSetData.accelerationPositions.Length >
                    gameEditorWindow.currentSetData.attemptsOfAccelerationInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfAccelerationInstantiate];
                    for (var i = 0; i < newPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.accelerationPositions[i];
                    gameEditorWindow.currentSetData.accelerationPositions = newPositions;
                }
                else if (gameEditorWindow.currentSetData.accelerationPositions.Length <
                         gameEditorWindow.currentSetData.attemptsOfAccelerationInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfAccelerationInstantiate];
                    for (var i = 0; i < gameEditorWindow.currentSetData.accelerationPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.accelerationPositions[i];

                    gameEditorWindow.currentSetData.accelerationPositions = newPositions;
                }
            }

            GUILayout.BeginVertical("box");
            
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("");
            GUILayout.EndHorizontal();
            GUILayout.Label("L");
            GUILayout.Label("G");
            GUILayout.Label("H");
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("Spawn on");
            GUILayout.EndHorizontal();
            
            gameEditorWindow.currentSetData.spawnAccelerationOnLet =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnAccelerationOnLet);
            gameEditorWindow.currentSetData.spawnAccelerationOnGround =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnAccelerationOnGround);
            gameEditorWindow.currentSetData.spawnAccelerationOnHole =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnAccelerationOnHole);
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            for (var i = 0; i < gameEditorWindow.currentSetData.attemptsOfAccelerationInstantiate; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
                GUILayout.Label("Platform index");
                GUILayout.EndHorizontal();
                gameEditorWindow.currentSetData.accelerationPositions[i] =
                    EditorGUILayout.IntField(gameEditorWindow.currentSetData.accelerationPositions[i]);
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }

        private void DrawShieldRandomSettings()
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Shield");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.attemptsOfShieldInstantiate =
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.attemptsOfShieldInstantiate, 0, 5);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
            GUILayout.Label("Spawn chance");
            GUILayout.EndHorizontal();
            gameEditorWindow.currentSetData.spawnShieldChance = 
                EditorGUILayout.IntSlider(gameEditorWindow.currentSetData.spawnShieldChance, 0, 100);
            //GUILayout.Label("%", new GUIStyle{fixedWidth = 10, alignment = TextAnchor.MiddleCenter, fontSize = 16, border = new RectOffset(5, 0, 0, 0)});
            GUILayout.EndHorizontal();
            
            if (gameEditorWindow.currentSetData.shieldPositions.Length !=
                gameEditorWindow.currentSetData.attemptsOfShieldInstantiate)
            {
                if (gameEditorWindow.currentSetData.shieldPositions.Length >
                    gameEditorWindow.currentSetData.attemptsOfShieldInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfShieldInstantiate];
                    for (var i = 0; i < newPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.shieldPositions[i];
                    gameEditorWindow.currentSetData.shieldPositions = newPositions;
                }
                else if (gameEditorWindow.currentSetData.shieldPositions.Length <
                         gameEditorWindow.currentSetData.attemptsOfShieldInstantiate)
                {
                    var newPositions = new int[gameEditorWindow.currentSetData.attemptsOfShieldInstantiate];
                    for (var i = 0; i < gameEditorWindow.currentSetData.shieldPositions.Length; i++)
                        newPositions[i] = gameEditorWindow.currentSetData.shieldPositions[i];

                    gameEditorWindow.currentSetData.shieldPositions = newPositions;
                }
            }
            
            
            
            GUILayout.BeginVertical("box");
            
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("");
            GUILayout.EndHorizontal();
            GUILayout.Label("L");
            GUILayout.Label("G");
            GUILayout.Label("H");
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 110});
            GUILayout.Label("Spawn on");
            GUILayout.EndHorizontal();
            
            gameEditorWindow.currentSetData.spawnShieldOnLet =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnShieldOnLet);
            gameEditorWindow.currentSetData.spawnShieldOnGround =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnShieldOnGround);
            gameEditorWindow.currentSetData.spawnShieldOnHole =
                EditorGUILayout.Toggle(gameEditorWindow.currentSetData.spawnShieldOnHole);
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            for (var i = 0; i < gameEditorWindow.currentSetData.attemptsOfShieldInstantiate; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginHorizontal(new GUIStyle {fixedWidth = 100});
                GUILayout.Label("Platform index");
                GUILayout.EndHorizontal();
                gameEditorWindow.currentSetData.shieldPositions[i] =
                    EditorGUILayout.IntField(gameEditorWindow.currentSetData.shieldPositions[i]);
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }
    }
}