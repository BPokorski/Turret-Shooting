using Unity.Entities;

namespace Components.Bullet
{
    [InternalBufferCapacity(1)]
    public struct BulletHitElement : IBufferElementData
    {
        public Entity HitEntity;
    }
}