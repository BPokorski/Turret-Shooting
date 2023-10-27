using Components.Bullet;
using Components.Turret;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Aspects.Bullet
{
    public readonly partial struct BulletHitAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRW<BulletHitTarget> _bulletHitTarget;
        private readonly RefRW<LocalTransform> _transform;
        private readonly DynamicBuffer<BulletHitElement> _bulletHit;
        private readonly RefRO<BulletProperties> _bulletProperties;
        
        public bool BulletHitTarget
        {
            get => _bulletHitTarget.ValueRO.Value;
            set => _bulletHitTarget.ValueRW.Value = value;
        }

        public float3 BulletPosition => _transform.ValueRO.Position;
        private float Speed => _bulletProperties.ValueRO.Speed;
        
        public void Hit(PhysicsWorldSingleton physicsWorld,
            EntityCommandBuffer.ParallelWriter ecb,
            ComponentLookup<TurretLiveNumber> turretLiveNumberLookup,
            ComponentLookup<TurretHittable> turretHittableLookup, int sortKey)
        {
            var startRayPosition = new float3(BulletPosition.x, BulletPosition.y, BulletPosition.z);
            var endRayPosition = _transform.ValueRO.Forward() * Speed;
            var raycastInput = new RaycastInput
            {
                Start = startRayPosition,
                Filter = CollisionFilter.Default,
                End = endRayPosition
            };
            
            // Debug.DrawRay(new Vector3(startRayPosition.x, startRayPosition.y, startRayPosition.z),
            //     new Vector3(endRayPosition.x, endRayPosition.y, endRayPosition.z), Color.red, 1.0f);
            
            if (!physicsWorld.CastRay(raycastInput, out var closestHit)) return;
            var hitEntity = closestHit.Entity;
            if (!turretLiveNumberLookup.HasComponent(hitEntity)) return;
            if (!turretHittableLookup.IsComponentEnabled(hitEntity)) return;
            if (_bulletHit.Length > 1)
            {
                return;
            }
            for (int i = 0; i < _bulletHit.Length; i++)
            {
                if(_bulletHit[i].HitEntity.Equals(hitEntity)) return;
            }

            // Debug.Log("HIT Target");
            _bulletHit.Add(new BulletHitElement {HitEntity = hitEntity});
            BulletHitTarget = true;
            ecb.SetComponentEnabled<TurretLiveNumber>(sortKey, hitEntity, true);
        }
    }
}