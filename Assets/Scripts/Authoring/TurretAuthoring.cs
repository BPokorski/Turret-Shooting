using Components;
using Components.Turret;
using Turrets;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Serialization;

namespace Authoring
{
    public class TurretAuthoring : MonoBehaviour
    {
        [SerializeField] private TurretDataSO _turretDataSO;
        [field: SerializeField] public GameObject BulletSpawn { get; private set; }
        
        [field: SerializeField] public GameObject TurretCannon { get; private set; }
        public int LiveNumber => _turretDataSO.LiveNumber;
        public float SpawnTime => _turretDataSO.SpawnTime;
        public float ShootTime => _turretDataSO.ShootTime;

        public GameObject BulletPrefab => _turretDataSO.BulletPrefab;
        
        public class TurretBaker : Baker<TurretAuthoring>
        {
            public override void Bake(TurretAuthoring authoring)
            {
                Entity entity = this.GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new TurretProperties
                {
                    
                    SpawnTime = authoring.SpawnTime,
                    ShootTime = authoring.ShootTime,
                    BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
                    BulletSpawn = GetEntity(authoring.BulletSpawn, TransformUsageFlags.Dynamic),
                    TurretCannon = GetEntity(authoring.TurretCannon, TransformUsageFlags.Dynamic)
                    
                });
                
                AddComponent(entity, new DeltaRotationTimer
                {
                    Value = 0.0f
                });
                AddComponent(entity, new TurretShootTimer
                {
                    Value = authoring.ShootTime
                });
                
                AddComponent(entity, new TurretLiveNumber
                {
                    LiveNumber = authoring.LiveNumber
                });
                AddComponent(entity, new TurretTag
                {
                    
                });
                
                AddComponent(entity, new TurretSpawnTimer
                {
                    Value = 0.0f
                });
                
                AddComponent(entity, new TurretHittable
                {
                    
                });
                AddBuffer<TurretField>(entity);
            }
        }
    }
}