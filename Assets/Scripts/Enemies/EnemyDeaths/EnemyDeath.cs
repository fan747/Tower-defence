using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies.EnemyDeaths
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
            GameObject.Destroy(_enemy);
        }
    }
}
