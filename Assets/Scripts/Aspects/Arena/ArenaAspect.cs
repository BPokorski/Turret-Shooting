using Components.Arena;
using Unity.Entities;
using Unity.Mathematics;

namespace Aspects.Arena
{
    public readonly partial struct ArenaAspect: IAspect
    {
        public readonly Entity Entity;
        
        private readonly RefRO<ArenaProperties> _arenaProperties;
        private readonly RefRW<ArenaRandom> _arenaRandom;
        public int LevelSize => _arenaProperties.ValueRO.LevelSize; 
        public int TurretNumber => _arenaProperties.ValueRO.TurretNumber;
        public Entity FieldPrefab => _arenaProperties.ValueRO.FieldPrefab;
        public Entity TurretPrefab => _arenaProperties.ValueRO.TurretPrefab;
        
        public int2 GetRandomPositionXY()
        {
            return _arenaRandom.ValueRW.Value.NextInt2(MinCorner, MaxCorner);
            
        }

        public float GetRandomFloatValue(float minRange, float maxRange)
        {
            return _arenaRandom.ValueRW.Value.NextFloat(minRange, maxRange);
        }

        public int GetRandomIntValue(int minRange, int maxRange)
        {
            return _arenaRandom.ValueRW.Value.NextInt(minRange, maxRange);
        }
        public int2 IndexToCoordinates(int index, int height)
        {
            int2 coordinates = new int2
            {
                x = index / height,
                y = index % height
            };
            return coordinates;
        }

        public int CoordinatesToIndex(int2 coordinates, int height)
        {
            return height * coordinates.x + coordinates.y;
        }

        private int2 MinCorner => new int2(0, 0);
        private int2 MaxCorner => new int2(LevelSize, LevelSize);
    }
}