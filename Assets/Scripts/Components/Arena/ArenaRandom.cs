using Unity.Entities;
using Unity.Mathematics;

namespace Components.Arena
{
    public struct ArenaRandom : IComponentData
    {
        public Random Value;
    }
}