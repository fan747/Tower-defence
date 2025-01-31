using Assets.Scripts.Enemies;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Factories
{
    public interface IEnemyCreator
    {
        Enemy CreateEnemy(Vector3 position);
    }
}