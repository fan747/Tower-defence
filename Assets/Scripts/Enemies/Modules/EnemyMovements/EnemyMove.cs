using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies.Modules.EnemyMovements
{
    public abstract class EnemyMove : IEnemyMove
    {
        protected NavMeshAgent _navMeshAgent;
        protected bool _isCanMove = true;

        public EnemyMove(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public abstract void Move(Vector3 position);

        public virtual void StopMove()
        {
            if (!_navMeshAgent.isStopped)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.ResetPath();
                _isCanMove = false;
            }
        }
    }
}
