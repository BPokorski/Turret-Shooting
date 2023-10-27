using Aspects;
using Components.Turret;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using BulletHitAspect = Aspects.Bullet.BulletHitAspect;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    public partial struct HitBulletSystem : ISystem
    {
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // state.RequireForUpdate<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }
        public void OnStartRunning(ref SystemState state)
        {
            
        }

        public void OnStopRunning(ref SystemState state)
        {
            
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            var turretLiveNumberLookup = SystemAPI.GetComponentLookup<TurretLiveNumber>(true);
            var turretHittableLookup = SystemAPI.GetComponentLookup<TurretHittable>(true);
            var ecbSingleton = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            new HitBulletJob
            {
                PhysicsWorld = physicsWorld,
                TurretLiveNumberLookup = turretLiveNumberLookup,
                TurretHittableLookup = turretHittableLookup,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        public partial struct HitBulletJob: IJobEntity
        {
            [ReadOnly]
            public PhysicsWorldSingleton PhysicsWorld;
            [ReadOnly]
            public ComponentLookup<TurretLiveNumber> TurretLiveNumberLookup;
            [ReadOnly]
            public ComponentLookup<TurretHittable> TurretHittableLookup;
            public EntityCommandBuffer.ParallelWriter ECB;

            private void Execute(BulletHitAspect bulletAspect, [ChunkIndexInQuery] int sortKey)
            {
                bulletAspect.Hit(PhysicsWorld, ECB, TurretLiveNumberLookup, TurretHittableLookup, sortKey);
                // var bulletPosition = new float3(bulletAspect.BulletPosition.x, bulletAspect.BulletPosition.y, bulletAspect.BulletPosition.z + 0.5f);
                // var endPosition = new float3(bulletPosition.x, bulletPosition.y, bulletPosition.z + 0.25f);
                // var raycastInput = new RaycastInput
                // {
                //     Start = bulletPosition,
                //     Filter = CollisionFilter.Default,
                //     End = endPosition
                // };
                // if (!PhysicsWorld.CastRay(raycastInput, out var closestHit)) return;
                // var hitEntity = closestHit.Entity;
                // if (!TurretLiveNumberLookup.HasComponent(hitEntity)) return;
                // bulletAspect.BulletHitTarget = true;
                //
                // ECB.SetComponentEnabled<TurretLiveNumber>(sortKey, hitEntity, true);
                // Debug.Log($"hit: {closestHit.Entity}");

            }
        }

        
    }
}