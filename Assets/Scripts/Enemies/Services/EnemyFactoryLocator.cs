using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Data;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies.Services
{
    public class EnemyFactoryLocator
    {
        private readonly Dictionary<EnemyType, ICreator<Enemy>> _services = new Dictionary<EnemyType, ICreator<Enemy>>();

        public void Register<T>(EnemyType enemyType, T creator) where T : ICreator<Enemy>
        {
            if (creator == null)
            {
                Debug.LogError("An attempt to register an creator == null");
                return;
            }

            if (_services.ContainsKey(enemyType))
            {
                Debug.LogError("An attempt to register an already registered service");
                return;
            }

            _services.Add(enemyType, creator);
        }

        public void Unregister(EnemyType enemyType)
        {
            if (!_services.ContainsKey(enemyType))
            {
                Debug.LogError("An attempt to unregister an already unregistered creator");
                return;
            }

            _services.Remove(enemyType);
        }

        public ICreator<Enemy> Get(EnemyType enemyType)
        {
            if (!_services.ContainsKey(enemyType))
            {
                Debug.LogError($"An attempt to get an unregistered creator");
                throw new InvalidOperationException();
            }

            return _services[enemyType];
        }
    }
}