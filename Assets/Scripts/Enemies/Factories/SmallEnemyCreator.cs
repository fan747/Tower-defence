using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Configs;
using Assets.Scripts.Enemies.Modules.EnemyAttacks;
using Assets.Scripts.Enemies.Modules.EnemyMovements;
using Assets.Scripts.Factory;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Modules;
using Assets.Scripts.Services;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies.Factories
{
    public class SmallEnemyCreator : Creator, ICreator<Enemy>
    {
        private Transform _target;
        private Action _enemyDieAction;
        private EnemyConfig _enemyConfig;

        public SmallEnemyCreator(Transform target, Action enemyDieAction, AssetLoader assetLoader) : base(assetLoader)
        {
            _target = target;
            _enemyDieAction = enemyDieAction;
        }

        public async UniTask<Enemy> Create(Vector3 position)
        {
            _enemyConfig = await LoadConfig<EnemyConfig>();
            GameObject enemyInstantiate = UnityEngine.Object.Instantiate(_enemyConfig.Prefab, position, Quaternion.identity);

            var enemyComponent = enemyInstantiate.AddComponent<DefaultEnemy>();

            var navMeshAgent = enemyInstantiate.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = _enemyConfig.Speed;

            var attackTrigger = enemyInstantiate.GetComponent<CapsuleCollider>();
            attackTrigger.radius = _enemyConfig.AttackDistance;

            FootEnemyMove enemyMove = new(navMeshAgent);
            Health health = new(_enemyConfig.Health);
            Death enemyDeath = new(enemyInstantiate);
            MeleeAttack meleeAttack = new(_enemyConfig.Damage, _enemyConfig.AttackDistance, _enemyConfig.AttackCooldown);

            enemyComponent.Construct(enemyMove, health, _target, enemyDeath, meleeAttack, _enemyDieAction);
            return enemyComponent;
        }
    }
}
