using Unity.Entities;

namespace Components.Turret
{
    public struct RandomTurretProperties : IComponentData
    {
        public float RotationAngle;
        public float RotationTime;
    }
}