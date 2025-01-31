using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Modules.EnemyDeaths
{
    public class EnemyDeath : IEnemyDeath
    {
        private GameObject _enemy;

        public EnemyDeath(GameObject enemy)
        {
            _enemy = enemy;
        }

        public void Death()
        {
            UnityEngine.Object.Destroy(_enemy);
        }
    }
}
