using Components.Field;
using Components.Turret;
using Unity.Entities;
using Unity.Rendering;

namespace Aspects.Turret
{
    public readonly partial struct TurretHitAspect: IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<TurretProperties> _turretProperties;
        private readonly RefRW<TurretLiveNumber> _turretLiveNumber;
        
        private readonly EnabledRefRW<TurretLiveNumber> _enabledLiveNumber;
        
        private readonly DynamicBuffer<TurretField> _turretFieldBuffer;
        public int TurretLiveNumber
        {
            get => _turretLiveNumber.ValueRO.LiveNumber;
            set => _turretLiveNumber.ValueRW.LiveNumber = value;
        }

        public Entity TurretCannon => _turretProperties.ValueRO.TurretCannon;

        public Entity TurretPlatform => _turretProperties.ValueRO.TurretPlatform;
        public void Hit(EntityCommandBuffer.ParallelWriter ECB, int sortKey)
        {
            if (!_enabledLiveNumber.ValueRO) return;
            TurretLiveNumber -= 1;

            if (TurretLiveNumber > 0)
            {
                if (_turretFieldBuffer.Length < 1) return;
                ECB.SetComponentEnabled<FieldPosition>(sortKey, _turretFieldBuffer[0].FieldEntity, true);
                _turretFieldBuffer.Clear();
                
                ECB.SetComponentEnabled<TurretSpawnTimer>(sortKey, Entity, true);
                
                ECB.SetComponentEnabled<TurretRotateTimer>(sortKey, Entity, false);
                ECB.SetComponentEnabled<TurretShootTimer>(sortKey, Entity, false);
                ECB.SetComponentEnabled<TurretHittable>(sortKey, Entity, false);
                ECB.AddComponent<DisableRendering>(sortKey, Entity);
                ECB.AddComponent<DisableRendering>(sortKey, TurretCannon);
                ECB.AddComponent<DisableRendering>(sortKey, TurretPlatform);
                _enabledLiveNumber.ValueRW = false;
                
            }
            else
            {
                ECB.DestroyEntity(sortKey, Entity);
            }

        }
    }
}