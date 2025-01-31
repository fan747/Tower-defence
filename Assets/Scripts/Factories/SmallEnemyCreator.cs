using Assets.Scripts.Configs;
using Assets.Scripts.Enemies;
using Assets.Scripts.Modules;
using Assets.Scripts.Modules.EnemyDeaths;
using Assets.Scripts.Modules.EnemyMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Factories
{
    public class SmallEnemyCreator : EnemyCreator
    {
        private Transform _target;
        private Action _enemyDieAction;

        public SmallEnemyCreator(Transform target, Action enemyDieAction)
        {
            _target = target;
            _enemyDieAction = enemyDieAction;
        }

        protected override string EnemyPath => "SmallEnemy";

        public override Enemy CreateEnemy(Vector3 position)
        {
            GameObject enemyInstantiate = InstantiatePrefab(position);
            var enemyComponent = enemyInstantiate.AddComponent<DefaultEnemy>();

            var navMeshAgent = enemyInstantiate.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = _enemyConfig.Speed;

            FootEnemyMove enemyMove = new(navMeshAgent);
            Health health = new(_enemyConfig.Health);
            EnemyDeath enemyDeath = new(enemyInstantiate);

            enemyComponent.Construct(enemyMove, health, _target, enemyDeath, _enemyDieAction);
            return enemyComponent;
        }
    }
}
