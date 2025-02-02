using Assets.Scripts.Enemies;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Factories
{
    public interface IEnemyCreator
    {
        UniTask<Enemy> CreateEnemy(Vector3 position);
    }
}