using Assets.Scripts.Interfaces;
using Assets.Scripts.Modules;

namespace Assets.Scripts.Enemies.Modules.EnemyAttacks
{
    public class MeleeAttack : Attack
    {
        public MeleeAttack(float damage, float attackDistance, float attackCooldown) : base(damage, attackDistance, attackCooldown)
        {
        }

        protected override void ExecuteAttack(IAttackable target, float damage)
        {
            target.GiveAttack(damage);
        }
    }
}
