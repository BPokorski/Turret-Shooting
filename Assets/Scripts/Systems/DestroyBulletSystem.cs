using Aspects;
using Unity.Burst;
using Unity.Entities;
using BulletDestroyAspect = Aspects.Bullet.BulletDestroyAspect;

namespace Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(SpawnBulletsSystem))]
    public partial struct DestroyBulletSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new DestroyBulletJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        public partial struct DestroyBulletJob: IJobEntity
        {
            public float DeltaTime;
            public EntityCommandBuffer.ParallelWriter ECB;

            private void Execute(BulletDestroyAspect bulletDestroy, [ChunkIndexInQuery] int sortKey)
            {
                bulletDestroy.BulletDestroyTimer -= DeltaTime;

                if (bulletDestroy.BulletDestroyTimer <= 0.0f || bulletDestroy.BulletHitTarget)
                {
                    ECB.DestroyEntity(sortKey, bulletDestroy.Entity);
                }
            }
        }
    }
}