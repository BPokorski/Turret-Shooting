using Components.Field;
using Components.Turret;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Aspects.Turret
{
    public readonly partial struct TurretSpawnAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<TurretProperties> _turretProperties;
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<TurretSpawnTimer> _turretSpawnTimer;
        
        private readonly EnabledRefRW<TurretSpawnTimer> _enabledSpawnTimer;
        
        private readonly DynamicBuffer<TurretField> _turretFieldBuffer;

        public float TurretSpawnTimer
        {
            get => _turretSpawnTimer.ValueRO.Value;
            set => _turretSpawnTimer.ValueRW.Value = value;
        }
        public void SetPosition(float3 position)
        {
            _transform.ValueRW.Position = position;
        }
        
        public float TurretSpawnTime => _turretProperties.ValueRO.SpawnTime;

        public Entity TurretCannon => _turretProperties.ValueRO.TurretCannon;

        public bool TimeToSpawn => TurretSpawnTimer <= 0.0f;

        public void Spawn(float3 position, Entity fieldEntity, EntityCommandBuffer.ParallelWriter ECB, int sortKey)
        {
            
            SetPosition(position);
            ECB.SetComponentEnabled<FieldPosition>(sortKey, fieldEntity, false);//;<FieldPosition>(sortKey, fieldEntity, false);
            var currentFieldEntity = new TurretField {FieldEntity = fieldEntity};
            ECB.AppendToBuffer(sortKey, Entity, currentFieldEntity);
            // _turretFieldBuffer.Add(currentFieldEntity);
            TurretSpawnTimer = TurretSpawnTime;
            // ECB.SetComponentEnabled<TurretSpawnTimer>(sortKey, Entity, false);
            // _enabledSpawnTimer.ValueRW = false;
            ECB.SetComponentEnabled<TurretRotateTimer>(sortKey, Entity, true);
            // _enabledRotateTimer.ValueRW = true;
            // _enabledShootTimer.ValueRW = true;
            ECB.SetComponentEnabled<TurretShootTimer>(sortKey, Entity, true);
            ECB.SetComponentEnabled<TurretHittable>(sortKey, Entity, true);
            ECB.RemoveComponent<DisableRendering>(sortKey, Entity);
            ECB.RemoveComponent<DisableRendering>(sortKey, TurretCannon);
            _enabledSpawnTimer.ValueRW = false;
            // ECB.SetComponentEnabled<TurretLiveNumber>(sortKey, Entity, true);
        }
    }
}