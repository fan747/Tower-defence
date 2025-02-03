using Assets.Scripts.Enemies.Modules.EnemyMovements;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Modules;
using System;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour, IAttackable, ICreatable
    {
        private const string TowerTag = "Tower";
        private IEnemyMove _enemyMove;
        private Health _health;
        private Transform _target;
        private Action _dieAction;
        private IDeath _enemyDeath;
        private IAttack _attack;

        public void Construct(IEnemyMove enemyMove, Health health, Transform target, IDeath enemyDeath, IAttack attack, Action dieAction)
        {
            _enemyMove = enemyMove;
            _health = health;
            _enemyDeath = enemyDeath;
            _health.DieAction += _enemyDeath.ExecuteDeath;
            _target = target;
            _dieAction = dieAction;
            _attack = attack;
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
            _health.DieAction -= _enemyDeath.ExecuteDeath;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(TowerTag))
            {
                _enemyMove.StopMove();
                _attack.AttackCollider(other);
            }
        }

    }
}
