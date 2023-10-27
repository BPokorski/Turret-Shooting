using Aspects;
using Components;
using Components.Arena;
using Components.Turret;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using ArenaAspect = Aspects.Arena.ArenaAspect;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct InitTurretSystem: ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ArenaProperties>();
            
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var arenaEntity = SystemAPI.GetSingletonEntity<ArenaProperties>();
            var arena = SystemAPI.GetAspect<ArenaAspect>(arenaEntity);

            var turretNumber = arena.TurretNumber;
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            for (int i = 0; i < turretNumber; i++)
            {
                var newTurret = ecb.Instantiate(arena.TurretPrefab);
                
                var rotationAngle = arena.GetRandomFloatValue(0.01f, 360.0f);
                var rotationTime = arena.GetRandomFloatValue(0.01f, 1.0f);
                ecb.SetComponent(newTurret, new LocalTransform
                {
                    // Position = position,
                    Scale = 0.35f
                });
                ecb.AddComponent(newTurret, new RandomTurretProperties
                {
                    RotationAngle = rotationAngle,
                    RotationTime = rotationTime
                });
                
                ecb.AddComponent(newTurret, new TurretRotateTimer
                {
                    Value = rotationTime
                });
                
                ecb.SetComponentEnabled<TurretRotateTimer>(newTurret, false);
                ecb.SetComponentEnabled<TurretShootTimer>(newTurret, false);
                ecb.SetComponentEnabled<TurretLiveNumber>(newTurret, false);
                ecb.SetComponentEnabled<TurretSpawnTimer>(newTurret, true);
                ecb.SetComponentEnabled<TurretHittable>(newTurret, false);
            }
            ecb.Playback(state.EntityManager);
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
    }
}