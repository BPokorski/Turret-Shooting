using Unity.Entities;
using Unity.Mathematics;

namespace Components.Bullet
{
    public struct BulletVelocity : IComponentData
    {
        public float3 Velocity;
    }
}