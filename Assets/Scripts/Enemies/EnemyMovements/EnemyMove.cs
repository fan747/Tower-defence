using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies.EnemyMovements
{
    public abstract class EnemyMove : IEnemyMove
    {
        protected NavMeshAgent _navMeshAgent;

        public EnemyMove(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public abstract void Move(Vector3 position);
    }
}
