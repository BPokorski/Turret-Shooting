using Aspects;
using Unity.Burst;
using Unity.Entities;
using TurretRotationAspect = Aspects.Turret.TurretRotationAspect;

namespace Systems
{
    [BurstCompile]
    // [UpdateAfter(typeof(InitTurretSystem))]
    public partial struct RotateTurretSystem : ISystem
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
            new RotateTurretJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
    }
    [BurstCompile]
    public partial struct RotateTurretJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        [BurstCompile]
        private void Execute(TurretRotationAspect turretRotation, [ChunkIndexInQuery] int sortKey)
        {
            turretRotation.TurretRotateTimer -= DeltaTime;
            if (!turretRotation.TimeToRotateTurret) return;
            turretRotation.TurretRotateTimer = turretRotation.RotationTime;
            
            turretRotation.Rotate(DeltaTime);
        }
    }
}