using Unity.Entities;

namespace Components.Turret
{
    public struct TurretSpawnTimer : IComponentData, IEnableableComponent
    {
        public float Value;
    }
}