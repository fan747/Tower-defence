using Assets.Scripts.Configs;
using Assets.Scripts.Data;
using Assets.Scripts.Modules.EnemyDeaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Services
{
    public class EnemyWaveSwitcher : IInitializable, IDisposable
    {
        private EnemyFactoryLocator _enemyFactoryLocator;
        private EnemySpawner _enemySpawner;
        private AssetLoader _wavesConfigAssetLoader;
        private EnemyWaveConfig _enemyWaveConfig;
        private int _currentWave = 0;

        public EnemyWaveSwitcher(string enemyWaveConfigPath, EnemyFactoryLocator enemyFactoryLocator, EnemySpawner enemySpawner)
        {
            _wavesConfigAssetLoader = new(enemyWaveConfigPath);
            _enemyFactoryLocator = enemyFactoryLocator;
            _enemySpawner = enemySpawner;
        }

        public void NextWave()
        {
            if (_currentWave >= _enemyWaveConfig.EnemyWaves.Count)
            {
                return;
            }

            _currentWave++;

            foreach (var i in _enemyWaveConfig.EnemyWaves[_currentWave].WaveData)
            {
                var factory = _enemyFactoryLocator.Get(i.enemyType);
                _enemySpawner.AddEnemyToSpawn(factory, i.count);
            }
        }

        public async void Initialize()
        {
            _enemyWaveConfig = await _wavesConfigAssetLoader.Load<EnemyWaveConfig>();
        }

        public void Dispose()
        {
            _wavesConfigAssetLoader.Unload();
        }
    }
}
