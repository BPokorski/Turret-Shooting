using Unity.Entities;
using Unity.Mathematics;

namespace Components.Turret
{
    public struct BulletSpawnPosition : IComponentData
    {
        public float3 SpawnPosition;
    }
}