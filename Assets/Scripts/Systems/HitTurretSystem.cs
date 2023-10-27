using Aspects;
using Unity.Burst;
using Unity.Entities;
using TurretHitAspect = Aspects.Turret.TurretHitAspect;

namespace Systems
{
    [BurstCompile]
    public partial struct HitTurretSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new HitTurretJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            }.ScheduleParallel();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        public partial struct HitTurretJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter ECB;

            private void Execute(TurretHitAspect turretHitAspect, [ChunkIndexInQuery] int sortKey)
            {

                turretHitAspect.Hit(ECB, sortKey);
            }
        }
    }
}