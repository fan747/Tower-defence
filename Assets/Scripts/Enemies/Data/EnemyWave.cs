using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies.Data
{
    [Serializable]
    public class EnemyWave
    {
        [SerializeField] private List<WaveData> _waveData = new();

        public List<WaveData> WaveData { get => _waveData; }
    }
}
