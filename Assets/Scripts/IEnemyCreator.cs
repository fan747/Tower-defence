using Assets.Scripts.Enemies;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IEnemyCreator
    {
        Enemy CreateEnemy(Vector3 position); 
    }
}