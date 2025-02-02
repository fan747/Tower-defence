using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class EnemyWave
    {
        [SerializeField] private List<WaveData> _waveData = new();

        public List<WaveData> WaveData { get => _waveData; }
    }
}
