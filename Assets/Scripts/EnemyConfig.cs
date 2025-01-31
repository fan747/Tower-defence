using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig/New EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private GameObject _prefab;

        public float Health { get => _health; }
        public float Speed { get => _speed; }
        public float Damage { get => _damage; }
        public float AttackCooldown { get => _attackCooldown; }
        public GameObject Prefab { get => _prefab; }
    }
}