using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Modules
{
    public class Death : IDeath
    {
        private GameObject _enemy;

        public Death(GameObject enemy)
        {
            _enemy = enemy;
        }

        public void ExecuteDeath()
        {
            Object.Destroy(_enemy);
        }
    }
}
