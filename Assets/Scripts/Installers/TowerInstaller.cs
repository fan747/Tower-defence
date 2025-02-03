using Assets.Scripts.Interfaces;
using Assets.Scripts.Modules;
using Assets.Scripts.Towers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private Tower _tower;
        public override void InstallBindings()
        {
            Container.Bind<IHealth>().To<Health>().FromNew().AsCached().WithArguments(10f).WhenInjectedInto<Tower>();
            Container.Bind<IDeath>().To<Death>().FromNew().AsCached().WithArguments(_tower.gameObject).WhenInjectedInto<Tower>();
            Container.Bind<Tower>().FromInstance(_tower).AsSingle();
        }
    }
}