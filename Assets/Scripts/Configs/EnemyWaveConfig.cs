using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyWaveConfig", menuName = "EnemyWaveConfig/New EnemyWaveConfig")]
    public class EnemyWaveConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyWave> _enemyWaves = new List<EnemyWave>();

        public List<EnemyWave> EnemyWaves { get => _enemyWaves; }
    }
}
