using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFactoryLocator
{
    private readonly Dictionary<EnemyType, IEnemyCreator> _services = new Dictionary<EnemyType, IEnemyCreator>();

    public void Register<T>(EnemyType enemyType, T creator) where T : IEnemyCreator
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

    public T Get<T>(EnemyType enemyType) where T : IEnemyCreator
    {
        if (!_services.ContainsKey(enemyType))
        {
            Debug.LogError($"An attempt to get an unregistered creator");
            throw new InvalidOperationException();
        }

        return (T)_services[enemyType];
    }
}