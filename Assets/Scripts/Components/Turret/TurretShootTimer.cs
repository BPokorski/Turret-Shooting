using Unity.Entities;

namespace Components.Turret
{
    public struct TurretShootTimer : IComponentData, IEnableableComponent
    {
        public float Value;
    }
}