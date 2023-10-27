using Unity.Entities;

namespace Components.Turret
{
    [InternalBufferCapacity(1)]
    public struct TurretField : IBufferElementData
    {
        public Entity FieldEntity;
    }
}