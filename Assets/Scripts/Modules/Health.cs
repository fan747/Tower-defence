using Assets.Scripts.Interfaces;
using System;

namespace Assets.Scripts.Modules
{
    public class Health : IHealth
    {
        public event Action DieAction;
        private float _health;

        public Health(float health)
        {
            _health = health;
        }

        public void TakeHealth(float health = 1)
        {
            if (_health > 0) {
                _health -= health;
                return;
            }
            DieAction?.Invoke();
            
        }
    }
}