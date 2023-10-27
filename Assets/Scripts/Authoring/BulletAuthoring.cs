using System;
using Components;
using Components.Bullet;
using Components.Turret;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Authoring
{
    public class BulletAuthoring : MonoBehaviour
    {
        [field: SerializeField]
        [field: Range(5.0f, 50f)]
        public float Speed { get; private set; } = 5.0f;

        [field: SerializeField]
        [field: Range(1.0f, 7.5f)]
        public float LiveTime { get; private set; } = 4.0f;
        
        public class BulletBaker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new BulletProperties
                {
                    Speed = authoring.Speed
                });
                
                AddComponent(entity, new BulletDestroyTimer
                {
                    Value = authoring.LiveTime
                });
                
                AddComponent(entity, new BulletHitTarget
                {
                    Value = false
                });

                AddBuffer<BulletHitElement>(entity);
            }
        }
    }
}