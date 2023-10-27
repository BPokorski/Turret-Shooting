using Aspects;
using Components;
using Components.Arena;
using Components.Field;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using ArenaAspect = Aspects.Arena.ArenaAspect;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnFieldsSystem : ISystem
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

            var fieldsNumber = arena.LevelSize * arena.LevelSize;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            for (int i = 0; i < fieldsNumber; i++)
            {
                var newField = ecb.Instantiate(arena.FieldPrefab);
                var position = arena.IndexToCoordinates(i, arena.LevelSize);
                ecb.SetComponent(newField, new LocalTransform
                {
                    Position = new float3(position.x, 0.0f, position.y),
                    Scale = 1f
                });
                
                ecb.AddComponent(newField, new FieldPosition
                {
                    X = position.x,
                    Y = position.y
                });
                
                ecb.SetComponentEnabled<FieldPosition>(newField, true);
            }

            // arena.ArenaFields = worldFields.ToArray(Allocator.Persistent);
            ecb.Playback(state.EntityManager);
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
    }
}