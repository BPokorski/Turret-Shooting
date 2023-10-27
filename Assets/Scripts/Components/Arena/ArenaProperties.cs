using Unity.Entities;

namespace Components.Arena
{
    public struct ArenaProperties : IComponentData
    {
        public int LevelSize;
        public int TurretNumber;
        public Entity FieldPrefab;
        public Entity TurretPrefab;
    }
}