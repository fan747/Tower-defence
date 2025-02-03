using Assets.Scripts.Enemies;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ICreator<T> where T : ICreatable
    {
        UniTask<T> Create(Vector3 position);
    }
}