using Assets.Scripts.Enemies.Services;
using Assets.Scripts.Services;
using System;
using Zenject;

namespace Assets.Scripts.Enemies.Controllers
{
    public class EnemyWaveController : ITickable, IDisposable
    {
        private EnemyWaveSwitcher _enemyWaveSwitcher;
        public Action EnemyDieAction;
        private Counter _enemyCounter = new();

        public EnemyWaveController(EnemyWaveSwitcher enemyWaveSwitcher)
        {
            _enemyWaveSwitcher = enemyWaveSwitcher;
            EnemyDieAction += _enemyCounter.RemoveOneCount;
        }

        public async void Tick()
        {
            if (_enemyCounter.IsNoCount)
            {
                _enemyCounter.AddCount(await _enemyWaveSwitcher.NextWave());
            }
        }

        public void Dispose()
        {
            EnemyDieAction -= _enemyCounter.RemoveOneCount;
        }
    }
}
