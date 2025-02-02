using Assets.Scripts.Configs;
using Assets.Scripts.Enemies;
using Assets.Scripts.Services;
using Cysharp.Threading.Tasks;
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
    public abstract class EnemyCreator : IEnemyCreator, IDisposable
    {
        protected abstract string EnemyPath { get; }

        protected AssetLoader AssetLoader => new(EnemyPath);

        protected EnemyConfig _enemyConfig;

        protected async UniTask<GameObject> InstantiatePrefab(Vector3 position)
        {
            if (_enemyConfig == null)
            {
                await LoadAssets();
            }

            return UnityEngine.Object.Instantiate(_enemyConfig.Prefab, position, Quaternion.identity);
        }

        public abstract UniTask<Enemy> CreateEnemy(Vector3 position);

        public void Dispose()
        {
            AssetLoader.Unload();
        }

        public async UniTask LoadAssets()
        {
            _enemyConfig = await AssetLoader.Load<EnemyConfig>();
        }
    }
}
