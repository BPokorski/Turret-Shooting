using Unity.Entities;

namespace Components.Turret
{
    public struct TurretLiveNumber : IComponentData, IEnableableComponent
    {
        public int LiveNumber;
    }
}