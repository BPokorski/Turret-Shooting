using Unity.Entities;

namespace Components.Turret
{
    public struct TurretRotateTimer : IComponentData, IEnableableComponent
    {
        public float Value;
    }
}