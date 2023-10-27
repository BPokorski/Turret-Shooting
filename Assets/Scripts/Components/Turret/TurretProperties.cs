using Unity.Entities;
using Unity.Mathematics;

namespace Components.Turret
{
    public struct TurretProperties : IComponentData
    {
        public float SpawnTime;
        public float ShootTime;
        public Entity BulletPrefab;
        public Entity BulletSpawn;
        public Entity TurretCannon;
    }
}