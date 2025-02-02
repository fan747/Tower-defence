using Assets.Scripts.Factories;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using Zenject;

namespace Assets.Scripts.Services
{
    public class EnemySpawner : ITickable
    {
        private List<Transform> _spawnPoints;
        private float _enemySpawnCooldown;
        private Queue<(IEnemyCreator, int)> _enemySpawnQueue = new();
        private bool _isSpawning = false;

        [Inject]
        public void Construct(List<Transform> spawnPoints, float enemySpawnCooldown)
        {
            _spawnPoints = spawnPoints;
            _enemySpawnCooldown = enemySpawnCooldown;
        }

        public void AddEnemyToSpawn(IEnemyCreator enemyCreator, int count = 1)
        {
            _enemySpawnQueue.Enqueue((enemyCreator, count));
        }

        public async void Tick()
        {
            if (_enemySpawnQueue.Count > 0 && !_isSpawning)
            {
                _isSpawning = true;

                while (_enemySpawnQueue.Count > 0)
                {
                    var (enemyCreator, count) = _enemySpawnQueue.Dequeue();

                    for (int i = 0; i < count; i++)
                    {
                        await enemyCreator.CreateEnemy(_spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position);
                        await UniTask.WaitForSeconds(_enemySpawnCooldown);
                    }
                }

                _isSpawning = false;
            }
        }
    }
}
