using Components.Bullet;
using Unity.Entities;

namespace Aspects.Bullet
{
    public readonly partial struct BulletDestroyAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRW<BulletDestroyTimer> _bulletDestroyTimer;
        private readonly RefRO<BulletHitTarget> _bulletHitTarget;


        public float BulletDestroyTimer
        {
            get => _bulletDestroyTimer.ValueRO.Value;
            set => _bulletDestroyTimer.ValueRW.Value = value;
        }

        public bool BulletHitTarget
        {
            get => _bulletHitTarget.ValueRO.Value;
        }
    }
}