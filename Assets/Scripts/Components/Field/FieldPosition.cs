using Unity.Entities;

namespace Components.Field
{
    public struct FieldPosition : IComponentData, IEnableableComponent
    {
        public int X;
        public int Y;
    }
}