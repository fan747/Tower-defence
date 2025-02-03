using Assets.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Towers
{
    public class Tower : MonoBehaviour, IAttackable, ICreatable
    {
        private IHealth _health;
        private IDeath _death;

        public void Construct(IHealth health, IDeath death)
        {
            _health = health;
            _death = death;
            _health.DieAction += _death.ExecuteDeath;
        }
        public void GiveAttack(float damage)
        {
            _health.TakeHealth(damage);
        }
    }
}
