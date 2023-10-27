using Components.Turret;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects.Turret
{
    public readonly partial struct TurretRotationAspect : IAspect
    {
        public readonly Entity TurretEntity;

        private readonly RefRO<TurretProperties> _turretProperties;
        private readonly RefRO<RandomTurretProperties> _randomTurretProperties;
        private readonly RefRW<TurretRotateTimer> _turretRotateTimer;
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<DeltaRotationTimer> _timer;
        private readonly EnabledRefRO<TurretRotateTimer> _enabledRotateTimer;
        public float TurretRotateTimer
        {
            get => _turretRotateTimer.ValueRO.Value;
            set => _turretRotateTimer.ValueRW.Value = value;
        }

        public float Timer
        {
            get => _timer.ValueRO.Value;
            set => _timer.ValueRW.Value = value;
        }
        
        public bool TimeToRotateTurret => TurretRotateTimer <= 0f;
        public float RotationTime => _randomTurretProperties.ValueRO.RotationTime;
        public float RotationAngle => _randomTurretProperties.ValueRO.RotationAngle;
        
        public void Rotate(float deltaTime)
        {
            if (!_enabledRotateTimer.ValueRO) return;
            Timer += deltaTime;
            // Debug.Log("ROTATE?!");
            var rotation = quaternion.RotateY(math.radians(RotationAngle));
            // // var floatUp = new float3(0.0f, 1.0f, 0.0f);
            var newRotation = math.mul(math.normalizesafe(_transform.ValueRO.Rotation), rotation);
            var normalised = math.normalizesafe(newRotation);
            // var smoothRot = math.lerp(new float3(oldRotation.value.x, oldRotation.value.y, oldRotation.value.z)
            //     , new float3(newRotation.value.x, newRotation.value.y, newRotation.value.z), deltaTime);
            _transform.ValueRW.Rotation = normalised;
            // _transform.ValueRW.Rotation = quaternion.RotateY(math.radians(RotationAngle));
            // _transform.ValueRW.Rotation = quaternion.RotateY(math.radians(RotationAngle) * Timer); 
        }
    }
}