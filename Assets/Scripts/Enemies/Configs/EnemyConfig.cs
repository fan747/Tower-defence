using UnityEngine;

namespace Assets.Scripts.Enemies.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig/New EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _attackDistance;
        [SerializeField] private GameObject _prefab;

        public float Health { get => _health; }
        public float Speed { get => _speed; }
        public float Damage { get => _damage; }
        public float AttackCooldown { get => _attackCooldown; }
        public GameObject Prefab { get => _prefab; }
        public float AttackDistance { get => _attackDistance; }
    }
}