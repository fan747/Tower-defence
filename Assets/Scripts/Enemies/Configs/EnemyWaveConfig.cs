using Assets.Scripts.Enemies.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies.Configs
{
    [CreateAssetMenu(fileName = "EnemyWaveConfig", menuName = "EnemyWaveConfig/New EnemyWaveConfig")]
    public class EnemyWaveConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyWave> _enemyWaves = new List<EnemyWave>();

        public List<EnemyWave> EnemyWaves { get => _enemyWaves; }
    }
}
