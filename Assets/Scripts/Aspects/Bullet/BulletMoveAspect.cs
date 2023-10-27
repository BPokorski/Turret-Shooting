using Components.Bullet;
using Unity.Entities;
using Unity.Transforms;

namespace Aspects.Bullet
{
    public readonly partial struct BulletMoveAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<BulletProperties> _bulletProperties;


        private float Speed => _bulletProperties.ValueRO.Speed;
        public void Move(float deltaTime)
        {
            _transform.ValueRW.Position += _transform.ValueRO.Forward() * Speed * deltaTime;
        }
    }
}