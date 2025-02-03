using Assets.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Modules
{
    public abstract class Attack : IAttack
    {
        private float _damage;
        private float _attackDistance;
        private IAttackable _attackable;
        private float _attackCooldown;
        private bool _isAttacking = false;

        public Attack(float damage, float attackDistance, float attackCooldown)
        {
            _damage = damage;
            _attackDistance = attackDistance;
            _attackCooldown = attackCooldown;
        }

        private async UniTask TryExecuteAttack(IAttackable target, float distance)
        {
            if (_isAttacking) return;

            if (distance <= _attackDistance)
            {
                _isAttacking = true;

                ExecuteAttack(target, _damage);
                await UniTask.WaitForSeconds(_attackCooldown);

                _isAttacking = false;
            }
        }

        public void AttackCollider(Collider collider)
        {
            if (_attackable == null)
            {
                if (!collider.TryGetComponent<IAttackable>(out _attackable))
                {
                    Debug.LogError("AttackCollider does not have IAttackable");
                    return;
                }
            }

            TryExecuteAttack(_attackable, _attackDistance).Forget();
        }

        protected abstract void ExecuteAttack(IAttackable target, float damage);
    }
}
