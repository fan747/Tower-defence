using UnityEngine;

namespace Assets.Scripts.Enemies.Modules.EnemyMovements
{
    public interface IEnemyMove
    {
        void Move(Vector3 position);
        void StopMove();
    }
}