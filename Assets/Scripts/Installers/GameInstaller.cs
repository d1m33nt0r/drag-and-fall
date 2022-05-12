using System.Collections.Generic;
using Data.Core;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        private Queue<PatternData> patternDataPool;
        
        public override void InstallBindings()
        {
            BindPatternDataPool();
        }

        private void BindPatternDataPool()
        {
            patternDataPool = new Queue<PatternData>();
            
            Container
                .Bind<Queue<PatternData>>()
                .FromInstance(patternDataPool)
                .AsSingle();
            
            for (var i = 0; i < 15; i++)
            {
                var patternData = new PatternData(12, patternDataPool);
                patternDataPool.Enqueue(patternData);
            }
        }
    }
}