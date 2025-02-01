using Assets.Scripts.Configs;
using Assets.Scripts.Data;
using Assets.Scripts.Modules.EnemyDeaths;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
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
        private UniTask<EnemyWaveConfig> _enemyWaveConfigTask;
        private bool _isConfigLoading = false;

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
                await LoadAssets();
            }

            if (_currentWave >= _enemyWaveConfig.EnemyWaves.Count)
            {
                return enemyCount;
            }


            foreach (var i in _enemyWaveConfig.EnemyWaves[_currentWave].WaveData)
            {
                Debug.Log(i.enemyType + ": " + i.count);
                var factory = _enemyFactoryLocator.Get(i.enemyType);
                _enemySpawner.AddEnemyToSpawn(factory, i.count);
                enemyCount += i.count;
            }

            Debug.Log(enemyCount);
            _currentWave++;

            return enemyCount;
        }

        public async UniTask LoadAssets()
        {
            if (_isConfigLoading)
            {
                _enemyWaveConfig = await _enemyWaveConfigTask;
                return;
            }

            _enemyWaveConfigTask = _wavesConfigAssetLoader.Load<EnemyWaveConfig>();
            _enemyWaveConfig = await _enemyWaveConfigTask;
            _isConfigLoading = true;
        }

        public void Dispose()
        {
            _wavesConfigAssetLoader.Unload();
        }

        public async void Initialize()
        {
            await LoadAssets();
        }
    }
}
