using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Modules.EnemyMovements
{
    public class FootEnemyMove : EnemyMove
    {
        public FootEnemyMove(NavMeshAgent navMeshAgent) : base(navMeshAgent)
        {
        }

        public override void Move(Vector3 position)
        {
            if (!_navMeshAgent.hasPath)
            {
                _navMeshAgent.SetDestination(position);
            }
        }
    }
}
