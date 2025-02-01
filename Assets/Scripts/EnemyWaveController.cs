using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class EnemyWaveController : ITickable, IDisposable
    {
        private EnemyWaveSwitcher _enemyWaveSwitcher;
        private int _enemyCount = 0;
        private bool _isLastWave = false;
        public Action EnemyDieAction;

        public EnemyWaveController(EnemyWaveSwitcher enemyWaveSwitcher)
        {
            _enemyWaveSwitcher = enemyWaveSwitcher;
            EnemyDieAction += UpdateEnemyCount;
            Debug.Log("EnemyWaveController was loaded");
        }

        public async void Tick()
        {
            if (_enemyCount == 0 && !_isLastWave)
            {
                _enemyCount = await _enemyWaveSwitcher.NextWave();
                _isLastWave = _enemyCount == 0;
            }
        }

        public void UpdateEnemyCount()
        {
            _enemyCount--;
        }

        public void Dispose()
        {
            EnemyDieAction -= UpdateEnemyCount;
        }
    }
}
