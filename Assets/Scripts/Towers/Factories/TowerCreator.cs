using Assets.Scripts.Enemies;
using Assets.Scripts.Factory;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Modules;
using Assets.Scripts.Services;
using Assets.Scripts.Towers.Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Towers.Factories
{
    public class TowerCreator : Creator, ICreator<Tower>
    {
        private TowerConfig _towerConfig;
        public TowerCreator(AssetLoader assetLoader) : base(assetLoader)
        {
        }

        public async UniTask<Tower> Create(Vector3 position)
        {
            _towerConfig = await _assetLoader.Load<TowerConfig>();
            var towerInstantiate = GameObject.Instantiate(_towerConfig.Prefab, position, Quaternion.identity);
            var towerComponent = towerInstantiate.AddComponent<Tower>();

            Health health = new(_towerConfig.Health);
            Death death = new(towerInstantiate.GameObject());

            towerComponent.Construct(health, death);
            _assetLoader.Unload();
            return towerComponent;
        }
    }
}
