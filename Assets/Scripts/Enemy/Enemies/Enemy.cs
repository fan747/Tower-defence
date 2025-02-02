using Assets.Scripts.Interfaces;
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
using Zenject;

namespace Assets.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour, IAttackable
    {
        private IEnemyMove _enemyMove;
        private Health _health;
        private Transform _target;
        private Action _dieAction;
        private IEnemyDeath _enemyDeath;

        public void Construct(IEnemyMove enemyMove, Health health, Transform target, IEnemyDeath enemyDeath, Action dieAction)
        {
            _enemyMove = enemyMove;
            _health = health;
            _enemyDeath = enemyDeath;
            _health.DieAction += _enemyDeath.Death;
            _target = target;
            _dieAction = dieAction;
        }

        public void GiveAttack(float damage)
        {
            _health.TakeHealth(damage);
        }

        private void Update()
        {
            _enemyMove.Move(_target.position);
        }

        private void OnDestroy()
        {
            _dieAction?.Invoke();
            _health.DieAction -= _enemyDeath.Death;
        }

    }
}
