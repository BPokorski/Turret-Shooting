using Components.Turret;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects.Turret
{
    public readonly partial struct TurretShootAspect : IAspect
    {
        public readonly Entity TurretEntity;
        private readonly RefRO<TurretProperties> _turretProperties;
        private readonly RefRW<TurretShootTimer> _turretShootTimer;
        private readonly RefRO<LocalTransform> _transform;

        private readonly EnabledRefRO<TurretShootTimer> _enabledShootTimer;
        // private readonly RefRO<>
        // private readonly RefRO<Buffer>

        public float TurretShootTimer
        {
            get => _turretShootTimer.ValueRO.Value;
            set => _turretShootTimer.ValueRW.Value = value;
        }

        public bool IsShootTimerEnabled => _enabledShootTimer.ValueRO;
        public Entity Bullet => _turretProperties.ValueRO.BulletPrefab;

        public Entity BulletSpawn => _turretProperties.ValueRO.BulletSpawn;
        public float TurretShootTime => _turretProperties.ValueRO.ShootTime;
        public bool TimeToShoot => TurretShootTimer <= 0.0f;
        // public LocalTransform Transform => 

        public quaternion TurretRotation => _transform.ValueRO.Rotation;

    }
}