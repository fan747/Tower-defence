using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Configs;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Services;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public abstract class Creator : IDisposable
    {
        protected AssetLoader _assetLoader;

        protected Creator(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public void Dispose()
        {
            _assetLoader.Unload();
        }

        public async UniTask<T> LoadConfig<T>()
        {
            return await _assetLoader.Load<T>();
        }
    }
}
