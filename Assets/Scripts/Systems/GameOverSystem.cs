using System;
using Components.Field;
using Components.Turret;
using EventArgs;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial class GameOverSystem : SystemBase
    {
        public Action<GameOverArgs> OnGameOver;
        private EntityQuery _query;
        
        protected override void OnCreate()
        {
           _query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<TurretTag>()
                .Build(this);
           Enabled = false;
        }

        protected override void OnUpdate()
        {
            if (_query.IsEmpty) return;
            var numberOfTurrets = _query.CalculateEntityCount();
            if (numberOfTurrets <=2)
            {
                OnGameOver?.Invoke(new GameOverArgs
                {
                });
                Enabled = false;
            }
        }
    }
}