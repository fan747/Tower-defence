using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies.Modules.EnemyMovements
{
    public class FootEnemyMove : EnemyMove
    {
        public FootEnemyMove(NavMeshAgent navMeshAgent) : base(navMeshAgent)
        {
        }

        public override void Move(Vector3 position)
        {
            if (!_navMeshAgent.hasPath && _isCanMove)
            {
                _navMeshAgent.SetDestination(position);
            }
        }
    }
}
