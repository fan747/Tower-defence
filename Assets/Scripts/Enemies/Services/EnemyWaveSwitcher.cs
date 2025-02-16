﻿using Assets.Scripts.Enemies.Configs;
using Assets.Scripts.Services;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Enemies.Services
{
    public class EnemyWaveSwitcher : IDisposable
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

        public async UniTask<int> NextWave()
        {
            int enemyCount = 0;

            if (_enemyWaveConfig == null)
            {
                await LoadWaveConfig();
            }

            foreach (var i in _enemyWaveConfig.EnemyWaves[_currentWave].WaveData)
            {
                var factory = _enemyFactoryLocator.Get(i.enemyType);
                _enemySpawner.AddEnemyToSpawn(factory, i.count);

                enemyCount += i.count;
            }

            if (_currentWave < _enemyWaveConfig.EnemyWaves.Count)
            {
                _currentWave++;
            }

            return enemyCount;
        }

        private async UniTask LoadWaveConfig()
        {
            _enemyWaveConfig = await _wavesConfigAssetLoader.Load<EnemyWaveConfig>();
        }

        public void Dispose()
        {
            _wavesConfigAssetLoader.Unload();
        }
    }
}
