using System.Collections.Generic;
using Data.Core;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        private Queue<GamePatternData> patternDataPool;
        
        public override void InstallBindings()
        {
            BindPatternDataPool();
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