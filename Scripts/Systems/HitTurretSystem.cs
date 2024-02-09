using Unity.Burst;
using Unity.Entities;
using Aspects.Turret;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(HitBulletSystem))]
    public partial struct HitTurretSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
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