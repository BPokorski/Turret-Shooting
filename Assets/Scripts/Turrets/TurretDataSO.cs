using UnityEngine;

namespace Turrets
{
    [CreateAssetMenu(fileName = "Turret Data", menuName = "ScriptableObjects/Turrets")]
    public class TurretDataSO : ScriptableObject
    {
        [field: SerializeField]
        [field: Range(1, 10)]
        public int LiveNumber { get; private set; } = 3;

        [field: SerializeField]
        [field: Range(1.0f, 10.0f)]
        public float SpawnTime { get; private set; } = 2.0f;

        [field: SerializeField]
        [field: Range(1.0f, 10.0f)]
        public float ShootTime { get; private set; } = 1.0f;
        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
        
    }
}