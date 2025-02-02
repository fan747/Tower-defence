using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
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
