using System;
using System.Collections.Generic;
using Data;
using Data.Core;
using Data.Core.Segments;
using Data.Core.Segments.Content;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core
{
    public class InfinityMode : MonoBehaviour
    {
        public InfinityData infinityData;

        [Inject] private Queue<GamePatternData> patternDataPool;
        
        private int randPlatformsAmount;
        private bool randIsInitialized;
        private int[] shieldPositions;
        private int[] magnetPositions;
        private int[] multiplierPositions;
        private int[] accelerationPositions;
        private int[] keyPositions;
        private int setPointer;
        private int patternPointer;

        private AvailablePositions availablePositions = new AvailablePositions();
        private AvailablePositions powerUpsAvailablePositions = new AvailablePositions();
        private AvailablePositions currenciesAvailablePositions = new AvailablePositions();
        
        public GamePatternData GetFirstPlatform()
        {
            var gamePatternData = new GamePatternData(12, patternDataPool);
            gamePatternData.Configure(new PatternData(12));
            return gamePatternData;
        }
        
        public void ResetPointers()
        {
            setPointer = 0;
            patternPointer = 0;
            randPlatformsAmount = 0;
            randIsInitialized = false;
        }
        
        public GamePatternData GetPatternData()
        {
            if (infinityData.patternSets[setPointer].isRandom)
            {
                availablePositions.Initialize();
                
                if (!randIsInitialized)
                {
                    randPlatformsAmount = Random.Range(infinityData.patternSets[setPointer].minPlatformsCount,
                        infinityData.patternSets[setPointer].maxPlatformsCount);

                    shieldPositions = infinityData.patternSets[setPointer].shieldPositions;
                    magnetPositions = infinityData.patternSets[setPointer].magnetPositions;
                    multiplierPositions = infinityData.patternSets[setPointer].multiplierPositions;
                    accelerationPositions = infinityData.patternSets[setPointer].accelerationPositions;
                    keyPositions = infinityData.patternSets[setPointer].keyPositions;
                    
                    randIsInitialized = true;
                }
                
                if (randPlatformsAmount > patternPointer + 1)
                {
                    var patternData1 = patternDataPool.Dequeue();
                    patternData1.isRandom = true;
                    patternData1.minHoleAmount = infinityData.patternSets[setPointer].minHoleAmount;
                    patternData1.maxHoleAmount = infinityData.patternSets[setPointer].maxHoleAmount;
                    patternData1.minLetAmount = infinityData.patternSets[setPointer].minLetAmount;
                    patternData1.maxLetAmount = infinityData.patternSets[setPointer].maxLetAmount;
                   
                    PlaceSegments(patternData1);
                    TryPlacePowerUps(patternData1);
                    TryPlaceCurrencies(patternData1, infinityData.patternSets[setPointer].currencyRandomSettings);
                    patternPointer++;
                    return patternData1;
                }

                if (infinityData.patternSets.Count > setPointer + 1)
                {
                    var patternData2 = patternDataPool.Dequeue();
                    patternData2.isRandom = true;
                    patternData2.minHoleAmount = infinityData.patternSets[setPointer].minHoleAmount;
                    patternData2.maxHoleAmount = infinityData.patternSets[setPointer].maxHoleAmount;
                    patternData2.minLetAmount = infinityData.patternSets[setPointer].minLetAmount;
                    patternData2.maxLetAmount = infinityData.patternSets[setPointer].maxLetAmount;
                    patternData2.isLast = true;
                    
                    PlaceSegments(patternData2);
                    TryPlacePowerUps(patternData2);
                    TryPlaceCurrencies(patternData2, infinityData.patternSets[setPointer].currencyRandomSettings);
                    randIsInitialized = false;
                    patternPointer = 0;
                    setPointer++;
                    return patternData2;
                }
                else
                {
                    var patternData3 = patternDataPool.Dequeue();
                    patternData3.isRandom = true;
                    patternData3.minHoleAmount = infinityData.patternSets[setPointer].minHoleAmount;
                    patternData3.maxHoleAmount = infinityData.patternSets[setPointer].maxHoleAmount;
                    patternData3.minLetAmount = infinityData.patternSets[setPointer].minLetAmount;
                    patternData3.maxLetAmount = infinityData.patternSets[setPointer].maxLetAmount;
                    patternData3.isLast = true;
                
                    PlaceSegments(patternData3);
                    TryPlacePowerUps(patternData3);
                    TryPlaceCurrencies(patternData3, infinityData.patternSets[setPointer].currencyRandomSettings);
                    randIsInitialized = false;
                    patternPointer = 0;
                    setPointer = 0;
                    return patternData3;
                }
                
            }
            else
            {
                var patternData = infinityData.patternSets[setPointer].patterns[patternPointer];
                var gamePatternData = patternDataPool.Dequeue();
                gamePatternData.Configure(patternData);
                if (infinityData.patternSets[setPointer].patterns.Count > patternPointer + 1)
                {
                    patternPointer++;
                    return gamePatternData;
                }

                if (infinityData.patternSets.Count > setPointer + 1)
                {
                    patternPointer = 0;
                    setPointer++;
                    return gamePatternData;
                }

                patternPointer = 0;
                setPointer = 0;
                return gamePatternData;
            }
        }

        private void TryPlaceCurrencies(GamePatternData patternData, CurrencyRandomSettings[] currencyRandomSettings)
        {
            for (var i = 0; i < currencyRandomSettings.Length; i++)
            {
                switch (currencyRandomSettings[i].accuracyLevel)
                {
                    case AccuracyLevel.Small:
                        if (currencyRandomSettings[i].rangeStart <= patternPointer &&
                            currencyRandomSettings[i].rangeEnd >= patternPointer)
                        {
                            var randomChance = Random.Range(0, 100);
                            if (randomChance > currencyRandomSettings[i].spawnChance) break;
                            
                            PrepareAvailablePositionsForCurrencies(patternData.segmentsData,
                                currencyRandomSettings[i].spawnOnLet, 
                                currencyRandomSettings[i].spawnOnGround, 
                                currencyRandomSettings[i].spawnOnHole);
                            
                            if (currencyRandomSettings[i].currencyType == CurrencyType.Coin) 
                                patternData.segmentsData[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = SegmentContent.Coin;
                            else 
                                patternData.segmentsData[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = SegmentContent.Crystal;
                        }
                        break;
                    case AccuracyLevel.Average:
                        if (currencyRandomSettings[i].rangeStart <= patternPointer &&
                            currencyRandomSettings[i].rangeEnd >= patternPointer)
                        {
                            for (var j = 0; j < 2; j++)
                            {
                                var randomChance = Random.Range(0, 100);
                                if (randomChance > currencyRandomSettings[i].spawnChance) continue;
                                
                                
                                PrepareAvailablePositionsForCurrencies(patternData.segmentsData, 
                                    currencyRandomSettings[i].spawnOnLet, 
                                    currencyRandomSettings[i].spawnOnGround, 
                                    currencyRandomSettings[i].spawnOnHole);
                                
                                if (currencyRandomSettings[i].currencyType == CurrencyType.Coin) 
                                    patternData.segmentsData[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = SegmentContent.Coin;
                                else 
                                    patternData.segmentsData[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = SegmentContent.Crystal;
                            }
                        }
                        break;
                    case AccuracyLevel.Large:
                        if (currencyRandomSettings[i].rangeStart <= patternPointer &&
                            currencyRandomSettings[i].rangeEnd >= patternPointer)
                        {
                            for (var j = 0; j < 3; j++)
                            {
                                var randomChance = Random.Range(0, 100);
                                if (randomChance > currencyRandomSettings[i].spawnChance) continue;
                                
                               
                                PrepareAvailablePositionsForCurrencies(patternData.segmentsData, 
                                    currencyRandomSettings[i].spawnOnLet, 
                                    currencyRandomSettings[i].spawnOnGround, 
                                    currencyRandomSettings[i].spawnOnHole);
                                
                                if (currencyRandomSettings[i].currencyType == CurrencyType.Coin) 
                                    patternData.segmentsData[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = SegmentContent.Coin;
                                else 
                                    patternData.segmentsData[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = SegmentContent.Crystal;
                            }
                        }
                        break;
                    case AccuracyLevel.Random:
                        if (currencyRandomSettings[i].rangeStart <= patternPointer ||
                            currencyRandomSettings[i].rangeEnd >= patternPointer)
                        {
                            var randCount = Random.Range(1, 4);
                            for (var j = 0; j < randCount; j++)
                            {
                                var randomChance = Random.Range(0, 100);
                                if (randomChance > currencyRandomSettings[i].spawnChance) continue;
                                
                                
                                PrepareAvailablePositionsForCurrencies(patternData.segmentsData, 
                                    currencyRandomSettings[i].spawnOnLet, 
                                    currencyRandomSettings[i].spawnOnGround, 
                                    currencyRandomSettings[i].spawnOnHole);
                                
                                if (currencyRandomSettings[i].currencyType == CurrencyType.Coin) 
                                    patternData.segmentsData[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = SegmentContent.Coin;
                                else 
                                    patternData.segmentsData[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = SegmentContent.Crystal;
                            }
                        }
                        break;
                }
            }
        }

        private void PrepareAvailablePositionsForCurrencies(SegmentData[] segmentDatas, bool spawnOnLet, bool spawnOnGround, bool spawnOnHole)
        {
            for (var i = 0; i < 12; i++)
            {
                if (segmentDatas[i].segmentContent != SegmentContent.None) continue;
                
                if (segmentDatas[i].segmentType == SegmentType.Ground && spawnOnGround)
                {
                    currenciesAvailablePositions[segmentDatas[i].positionIndex] = true;
                }
                
                else if (segmentDatas[i].segmentType == SegmentType.Hole && spawnOnHole)
                {
                    currenciesAvailablePositions[segmentDatas[i].positionIndex] = true;
                }
                
                else if (segmentDatas[i].segmentType == SegmentType.Let && spawnOnLet)
                {
                    currenciesAvailablePositions[segmentDatas[i].positionIndex] = true;
                }
                
                else
                {
                    currenciesAvailablePositions[segmentDatas[i].positionIndex] = false;
                }
            }
        }
        
        private void TryPlacePowerUps(GamePatternData patternData1)
        {
            powerUpsAvailablePositions.Initialize();
            
            for (var i = 0; i < shieldPositions.Length; i++)
            {
                if (patternPointer != shieldPositions[i]) continue;
                PrepareAvailablePositionsForBonuses(patternData1.segmentsData,
                    infinityData.patternSets[setPointer].spawnShieldOnLet,
                    infinityData.patternSets[setPointer].spawnShieldOnGround,
                    infinityData.patternSets[setPointer].spawnShieldOnHole);
                TryPlacePowerUp(patternData1.segmentsData, SegmentContent.Shield, infinityData.patternSets[setPointer].spawnShieldChance);
            }

            for (var i = 0; i < magnetPositions.Length; i++)
            {
                if (patternPointer != magnetPositions[i]) continue;
                PrepareAvailablePositionsForBonuses(patternData1.segmentsData,
                    infinityData.patternSets[setPointer].spawnMagnetOnLet,
                    infinityData.patternSets[setPointer].spawnMagnetOnGround,
                    infinityData.patternSets[setPointer].spawnMagnetOnHole);
                TryPlacePowerUp(patternData1.segmentsData, SegmentContent.Magnet, infinityData.patternSets[setPointer].spawnMagnetChance);
            }

            for (var i = 0; i < multiplierPositions.Length; i++)
            {
                if (patternPointer != multiplierPositions[i]) continue;
                PrepareAvailablePositionsForBonuses(patternData1.segmentsData,
                    infinityData.patternSets[setPointer].spawnMultiplierOnLet,
                    infinityData.patternSets[setPointer].spawnMultiplierOnGround,
                    infinityData.patternSets[setPointer].spawnMultiplierOnHole);
                TryPlacePowerUp(patternData1.segmentsData, SegmentContent.Multiplier, infinityData.patternSets[setPointer].spawnMultiplierChance);
            }

            for (var i = 0; i < accelerationPositions.Length; i++)
            {
                if (patternPointer != accelerationPositions[i]) continue;
                PrepareAvailablePositionsForBonuses(patternData1.segmentsData,
                    infinityData.patternSets[setPointer].spawnAccelerationOnLet,
                    infinityData.patternSets[setPointer].spawnAccelerationOnGround,
                    infinityData.patternSets[setPointer].spawnAccelerationOnHole);
                TryPlacePowerUp(patternData1.segmentsData, SegmentContent.Acceleration, infinityData.patternSets[setPointer].spawnAccelerationChance);
            }

            for (var i = 0; i < keyPositions.Length; i++)
            {
                if (patternPointer != keyPositions[i]) continue;
                PrepareAvailablePositionsForBonuses(patternData1.segmentsData,
                    infinityData.patternSets[setPointer].spawnKeyOnLet, 
                    infinityData.patternSets[setPointer].spawnKeyOnGround,
                    infinityData.patternSets[setPointer].spawnKeyOnHole);
                TryPlacePowerUp(patternData1.segmentsData, SegmentContent.Key, infinityData.patternSets[setPointer].spawnKeyChance);
            }
        }

        private void PrepareAvailablePositionsForBonuses(SegmentData[] segmentDatas, bool letIsAvailable, bool groundIsAvailable, bool holeIsAvailable)
        {
            for (var i = 0; i < 12; i++)
            {
                if (segmentDatas[i].segmentType == SegmentType.Ground && groundIsAvailable)
                {
                    powerUpsAvailablePositions[segmentDatas[i].positionIndex] = true;
                }
                
                else if (segmentDatas[i].segmentType == SegmentType.Hole && holeIsAvailable)
                {
                    powerUpsAvailablePositions[segmentDatas[i].positionIndex] = true;
                }
                
                else if (segmentDatas[i].segmentType == SegmentType.Let && letIsAvailable)
                {
                    powerUpsAvailablePositions[segmentDatas[i].positionIndex] = true;
                }

                else
                {
                    powerUpsAvailablePositions[segmentDatas[i].positionIndex] = false;
                }
            }
        }

        public void TryPlacePowerUp(SegmentData[] segmentDatas, SegmentContent segmentContent, int chance)
        {
            var randomChance = Random.Range(0, 100);
            if (randomChance >= chance) return;
            segmentDatas[powerUpsAvailablePositions.GetRandomPositionIndex()].segmentContent = segmentContent;
        }

        public void PlaceSegments(GamePatternData patternData)
        {
            PlaceHoleSegments(patternData);
            PlaceLetSegments(patternData);
            PlaceGroundSegments(patternData);
        }
        
        private void PlaceGroundSegments(GamePatternData patternData)
        {
            for (var j = 0; j < availablePositions.GetAvailablePositionCount(); j++)
            {
                var randomPosition = availablePositions.GetRandomPositionIndex();
                patternData.segmentsData[randomPosition].segmentType = SegmentType.Ground;
            }
        }

        private void PlaceLetSegments(GamePatternData patternData)
        {
            var randomAmount = Random.Range(patternData.minLetAmount, patternData.maxLetAmount);

            for (var j = 0; j < randomAmount; j++)
            {
                var randomPosition = availablePositions.GetRandomPositionIndex();
                patternData.segmentsData[randomPosition].segmentType = SegmentType.Let;
            }
        }

        private void PlaceHoleSegments(GamePatternData patternData)
        {
            var randomAmount = Random.Range(patternData.minHoleAmount, patternData.maxHoleAmount);

            for (var j = 0; j < randomAmount; j++)
            {
                var randomPosition = availablePositions.GetRandomPositionIndex();
                patternData.segmentsData[randomPosition].segmentType = SegmentType.Hole;
            }
        }
    }


    public class AvailablePositions
    {
        private bool[] availablePosition;

        public bool this[int index]
        {
            get => availablePosition[index];
            set => availablePosition[index] = value;
        }
        
        public AvailablePositions()
        {
            availablePosition = new bool[12];
            Initialize();
        }

        public void Initialize()
        {
            for (var i = 0; i < availablePosition.Length; i++)
                availablePosition[i] = true;
        }

        public int GetAvailablePositionCount()
        {
            var count = 0;
            
            for (var i = 0; i < availablePosition.Length; i++)
                if (availablePosition[i]) count++;

            return count;
        }

        public int GetRandomPositionIndex()
        {
            var randomPositionIndex = Random.Range(0, GetAvailablePositionCount());

            var j = -1;
            
            for (var i = 0; i < availablePosition.Length; i++)
            {
                if (!availablePosition[i]) continue;
                j++;
                if (j != randomPositionIndex) continue;
                availablePosition[i] = false;
                return i;
            }

            return j;
        }
    }
}