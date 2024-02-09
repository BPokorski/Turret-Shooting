using Components.Arena;
using Components.Field;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Aspects.Arena;
using Random = Unity.Mathematics.Random;
using Aspects.Turret;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct SpawnTurretSystem : ISystem
    {
        
        private EntityQuery _query;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ArenaProperties>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            _query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<FieldPosition>()
                .Build(ref state);
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var arenaEntity = SystemAPI.GetSingletonEntity<ArenaProperties>();
            var arena = SystemAPI.GetAspect<ArenaAspect>(arenaEntity);
            var localToWorldLookUp = SystemAPI.GetComponentLookup<FieldPosition>(false);
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            var deltaTime = SystemAPI.Time.DeltaTime;
            var randomSeed = (uint) arena.GetRandomIntValue(1, 1000);
            
            var freeFieldsEntities = _query.ToEntityArray(Allocator.TempJob);
            if (freeFieldsEntities.Length < 1) return;
            
            var fieldsHashMap = new NativeParallelHashSet<int>(freeFieldsEntities.Length,Allocator.TempJob);
            
            new SpawnTurretJob
            {
                FreeFieldEntities = freeFieldsEntities,
                FreeFieldPosition = localToWorldLookUp,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                RandomSeed = randomSeed,
                ResultFields = fieldsHashMap.AsParallelWriter(),
                DeltaTime = deltaTime
            }.Schedule();
            state.Dependency.Complete();
            
            freeFieldsEntities.Dispose();
            fieldsHashMap.Dispose();
            
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        public partial struct SpawnTurretJob: IJobEntity
        {
            [ReadOnly] public NativeArray<Entity> FreeFieldEntities;
            [ReadOnly] public ComponentLookup<FieldPosition> FreeFieldPosition;
            public uint RandomSeed;
            public EntityCommandBuffer.ParallelWriter ECB;
            public NativeParallelHashSet<int>.ParallelWriter ResultFields;
            public float DeltaTime;
            private void Execute(TurretSpawnAspect turret, [ChunkIndexInQuery] int sortKey)
            {
                if (FreeFieldEntities.Length < 1)
                {
                    return;
                }
                turret.TurretSpawnTimer -= DeltaTime;
                if (!turret.TimeToSpawn) return;
                
                var random = new Random(RandomSeed);
                var index = 0;
                var processing = true;
                do
                {
                    var randomPositionIndex = random.NextInt(0, FreeFieldEntities.Length);
                    if (ResultFields.Add(randomPositionIndex))
                    {
                        processing = false;
                        index = randomPositionIndex;
                    }
                        
                        
                } while (processing);
                var fieldEntity = FreeFieldEntities[index];
                var fieldPosition = FreeFieldPosition.GetRefROOptional(fieldEntity).ValueRO;
                var turretPosition = new float3(fieldPosition.X, 0.1f, fieldPosition.Y);
            
                turret.Spawn(turretPosition, fieldEntity, ECB, sortKey);
            }
        }
    }
}