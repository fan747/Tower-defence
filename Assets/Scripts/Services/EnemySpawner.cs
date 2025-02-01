using Assets.Scripts.Factories;
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
    public class EnemySpawner : MonoBehaviour
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

        public void Update()
        {
            if (_enemySpawnQueue.Count > 0 && !_isSpawning)
            {
                StartCoroutine(ProcessSpawnQueue(_enemySpawnCooldown));
            }
        }

        public void AddEnemyToSpawn(IEnemyCreator enemyCreator, int count = 1)
        {
            _enemySpawnQueue.Enqueue((enemyCreator, count));
            Debug.Log($"{count} enemy was added to spawn queue");
        }

        private IEnumerator ProcessSpawnQueue(float spawnCooldown)
        {
            _isSpawning = true;

            while (_enemySpawnQueue.Count > 0)
            {
                var (enemyCreator, count) = _enemySpawnQueue.Dequeue();

                for (int i = 0; i < count; i++)
                {
                    var enemy = enemyCreator.CreateEnemy(_spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position);
                    yield return new WaitForSeconds(spawnCooldown);
                }
            }
            _isSpawning = false;
        }
    }
}
