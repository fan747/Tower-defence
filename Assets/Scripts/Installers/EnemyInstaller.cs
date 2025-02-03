using Assets.Scripts.Enemies.Controllers;
using Assets.Scripts.Enemies.Data;
using Assets.Scripts.Enemies.Factories;
using Assets.Scripts.Enemies.Services;
using Assets.Scripts.Services;
using Assets.Scripts.Towers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private string _enemyWaveConfigPath;
        [SerializeField] private string _smallEnemyPath;
        [SerializeField] private List<Transform> _enemySpawnPoints;
        [SerializeField] private float _enemySpawnCooldown;
        public override void InstallBindings()
        {
            Container.Bind<float>().FromInstance(_enemySpawnCooldown).WhenInjectedInto<EnemySpawner>();
            Container.Bind<List<Transform>>().FromInstance(_enemySpawnPoints).WhenInjectedInto<EnemySpawner>();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().FromNew().AsSingle();
            Container.Bind<EnemyFactoryLocator>().FromNew().AsSingle();
            Container.Bind<string>().FromInstance(_enemyWaveConfigPath).AsCached().WhenInjectedInto<EnemyWaveSwitcher>();
            Container.BindInterfacesAndSelfTo<EnemyWaveSwitcher>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyWaveController>().FromNew().AsSingle();

            var towerTransform = Container.Resolve<Tower>().gameObject.transform;
            Container.Bind<AssetLoader>().FromNew().AsCached().WithArguments(_smallEnemyPath).WhenInjectedInto<SmallEnemyCreator>();
            Container.Bind<Transform>().FromInstance(towerTransform).AsCached().WhenInjectedInto<SmallEnemyCreator>();
            Container.Bind<Action>().FromResolveGetter<EnemyWaveController>(e => e.EnemyDieAction).AsCached().WhenInjectedInto<SmallEnemyCreator>();
            Container.BindInterfacesAndSelfTo<SmallEnemyCreator>().FromNew().AsSingle();

            Container.Resolve<EnemyFactoryLocator>()
                .Register(EnemyType.SmallEnemy, Container.Resolve<SmallEnemyCreator>());

        }
    }
}