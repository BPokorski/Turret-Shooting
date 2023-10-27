using Aspects;
using Unity.Burst;
using Unity.Entities;
using BulletMoveAspect = Aspects.Bullet.BulletMoveAspect;

namespace Systems
{
    [BurstCompile]
    // [UpdateAfter(typeof(SpawnBulletsSystem))]
    public partial struct MoveBulletSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new MoveBulletJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        public partial struct MoveBulletJob: IJobEntity
        {
            public float DeltaTime;


            private void Execute(BulletMoveAspect bulletAspect, [ChunkIndexInQuery] int sortKey)
            {
                bulletAspect.Move(DeltaTime);
            }
        }
    }
}