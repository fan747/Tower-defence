using Assets.Scripts.Controllers;
using Assets.Scripts.Factories;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private string _enemyWaveConfigPath;
    [SerializeField] private List<Transform> _enemySpawnPoints;
    [SerializeField] private float _enemySpawnCooldown;
    [SerializeField] private Transform _target;
    public override void InstallBindings()
    {
        Container.Bind<float>().FromInstance(_enemySpawnCooldown).WhenInjectedInto<EnemySpawner>();
        Container.Bind<List<Transform>>().FromInstance(_enemySpawnPoints).WhenInjectedInto<EnemySpawner>();
        Container.BindInterfacesAndSelfTo<EnemySpawner>().FromNew().AsSingle();
        Container.Bind<EnemyFactoryLocator>().FromNew().AsSingle();
        Container.Bind<string>().FromInstance(_enemyWaveConfigPath).AsCached().WhenInjectedInto<EnemyWaveSwitcher>();
        Container.BindInterfacesAndSelfTo<EnemyWaveSwitcher>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyWaveController>().FromNew().AsSingle();

        Container.Bind<Transform>().FromInstance(_target).AsCached().WhenInjectedInto<SmallEnemyCreator>();
        Container.Bind<Action>().FromResolveGetter<EnemyWaveController>(e  => e.EnemyDieAction).AsCached().WhenInjectedInto<SmallEnemyCreator>();
        Container.BindInterfacesAndSelfTo<SmallEnemyCreator>().FromNew().AsSingle();

        Container.Resolve<EnemyFactoryLocator>()
            .Register<SmallEnemyCreator>(Assets.Scripts.Data.EnemyType.SmallEnemy, Container.Resolve<SmallEnemyCreator>());

    }
}