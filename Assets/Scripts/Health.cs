using Assets.Scripts.Interfaces;
using System;

namespace Assets.Scripts
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
            _health -= health;
        }
    }
}