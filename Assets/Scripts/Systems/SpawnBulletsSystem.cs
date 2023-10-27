using Aspects;
using Components;
using Components.Arena;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using TurretShootAspect = Aspects.Turret.TurretShootAspect;

namespace Systems
{
    // [UpdateAfter(typeof(SpawnTurretSystem))]
    [BurstCompile]
    public partial struct SpawnBulletsSystem : ISystem
    {
        // private ComponentLookup<LocalToWorld> _localToWorldLookup;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ArenaProperties>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        
        public void OnStartRunning(ref SystemState state)
        {
            // _localToWorldLookup = SystemAPI.GetComponentLookup<LocalToWorld>(true);
        }

        public void OnStopRunning(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var localToWorldLookUp = SystemAPI.GetComponentLookup<LocalToWorld>(true);
            new SpawnBulletJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                LocalToWorldLookUp = localToWorldLookUp
            }.ScheduleParallel();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        
    }
    
    [BurstCompile]
    public partial struct SpawnBulletJob: IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        
        [ReadOnly]
        public ComponentLookup<LocalToWorld> LocalToWorldLookUp;
        [BurstCompile]
        private void Execute(TurretShootAspect turretShootAspect, [ChunkIndexInQuery] int sortKey)
        {
            
            turretShootAspect.TurretShootTimer -= DeltaTime;
            if (!turretShootAspect.TimeToShoot) return;
            if (!turretShootAspect.IsShootTimerEnabled) return;
            
            turretShootAspect.TurretShootTimer = turretShootAspect.TurretShootTime;
            
            var localTransform = LocalToWorldLookUp.GetRefROOptional(turretShootAspect.BulletSpawn).ValueRO;
            var newBullet = ECB.Instantiate(sortKey, turretShootAspect.Bullet);
            ECB.SetComponent(sortKey, newBullet, new LocalTransform
            {
                Position = localTransform.Position,
                Rotation = localTransform.Rotation,
                Scale = 0.1f
            });
            // var bullet = ECB.Instantiate(Arena.BulletPrefab);
            // turretRotation.(DeltaTime);
        }
    }
}