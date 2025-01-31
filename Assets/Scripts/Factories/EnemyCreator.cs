using Assets.Scripts.Configs;
using Assets.Scripts.Enemies;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Assets.Scripts.Factories
{
    public abstract class EnemyCreator : IEnemyCreator, IInitializable, IDisposable
    {
        protected abstract string EnemyPath { get; }

        protected AssetLoader AssetLoader => new(EnemyPath);

        protected EnemyConfig _enemyConfig;

        protected GameObject InstantiatePrefab(Vector3 position)
        {
            if (_enemyConfig == null)
            {
                Debug.LogError("EnemyConfig is not loaded");
                return default;
            }

            return UnityEngine.Object.Instantiate(_enemyConfig.Prefab, position, Quaternion.identity);
        }

        public abstract Enemy CreateEnemy(Vector3 position);

        public void Dispose()
        {
            AssetLoader.Unload();
        }

        public async void Initialize()
        {
            _enemyConfig = await AssetLoader.Load<EnemyConfig>();
        }
    }
}
