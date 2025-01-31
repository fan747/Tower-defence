using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using Zenject;

namespace Assets.Scripts
{
    public class EnemySpawner : IFixedTickable
    {
        private List<Transform> _spawnPoints;
        private float _enemySpawnCooldown;
        private Queue<(IEnemyCreator, int)> _enemySpawnQueue = new();

        public EnemySpawner(List<Transform> spawnPoints, float enemySpawnCooldown)
        {
            _spawnPoints = spawnPoints;
            _enemySpawnCooldown = enemySpawnCooldown;
        }

        public void FixedTick()
        {
            if (_enemySpawnQueue.Count > 0)
            {
                var enemyQueue = _enemySpawnQueue.Dequeue();

                for (int i = 0; i < enemyQueue.Item2; i++)
                {
                    var enemy = enemyQueue.Item1.CreateEnemy(_spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position);
                    enemy.StartCoroutine(WaitingSpawnCooldown(_enemySpawnCooldown));
                }
            }
        }

        public void AddEnemyToSpawn(IEnemyCreator enemyCreator, int count = 1)
        {
            _enemySpawnQueue.Enqueue((enemyCreator,count));
        }

        private IEnumerator WaitingSpawnCooldown(float spawnCooldown)
        {
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
