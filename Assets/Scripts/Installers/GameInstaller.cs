using System.Collections.Generic;
using Core;
using Data.Core;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        private Queue<GamePatternData> patternDataPool;
        [SerializeField] private SoundOfBuying soundOfBuying;
        [SerializeField] private GameManager gameManager;
        
        public override void InstallBindings()
        {
            BindPatternDataPool();
            BindSoundOfBuying();
            BindGameManager();
        }

        private void BindGameManager()
        {
            Container
                .Bind<GameManager>()
                .FromInstance(gameManager)
                .AsSingle();
        }

        private void BindSoundOfBuying()
        {
            Container
                .Bind<SoundOfBuying>()
                .FromInstance(soundOfBuying)
                .AsSingle();
        }

        private void BindPatternDataPool()
        {
            patternDataPool = new Queue<GamePatternData>();
            
            Container
                .Bind<Queue<GamePatternData>>()
                .FromInstance(patternDataPool)
                .AsSingle();
            
            for (var i = 0; i < 15; i++)
            {
                var patternData = new GamePatternData(12, patternDataPool);
                patternDataPool.Enqueue(patternData);
            }
        }
    }
}