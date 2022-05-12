using Core;
using Data.Core;
using Data.Core.Segments;
using UnityEngine;

namespace DafEditor.Editor
{
    public class Generator
    {
        private AvailablePositions availablePositions = new AvailablePositions();
        
        public void PlaceSegments(PatternData patternData, RandomGenerationProfile randomGenerationProfile)
        {
            availablePositions.Initialize();
            PlaceHoleSegments(patternData, randomGenerationProfile);
            PlaceLetSegments(patternData, randomGenerationProfile);
            PlaceGroundSegments(patternData);
        }
        
        private void PlaceGroundSegments(PatternData patternData)
        {
            for (var j = 0; j < availablePositions.GetAvailablePositionCount(); j++)
            {
                var randomPosition = availablePositions.GetRandomPositionIndex();
                patternData.segmentsData[randomPosition].segmentType = SegmentType.Ground;
            }
        }

        private void PlaceLetSegments(PatternData patternData, RandomGenerationProfile randomGenerationProfile)
        {
            var randomAmount = Random.Range(randomGenerationProfile.minLetAmount, randomGenerationProfile.maxLetAmount);

            for (var j = 0; j < randomAmount; j++)
            {
                var randomPosition = availablePositions.GetRandomPositionIndex();
                patternData.segmentsData[randomPosition].segmentType = SegmentType.Let;
            }
        }

        private void PlaceHoleSegments(PatternData patternData, RandomGenerationProfile randomGenerationProfile)
        {
            var randomAmount = Random.Range(randomGenerationProfile.minHoleAmount, randomGenerationProfile.maxHoleAmount);

            for (var j = 0; j < randomAmount; j++)
            {
                var randomPosition = availablePositions.GetRandomPositionIndex();
                patternData.segmentsData[randomPosition].segmentType = SegmentType.Hole;
            }
        }
    }
}