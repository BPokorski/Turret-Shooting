using System;
using Aspects;
using Components;
using Components.Arena;
using Level.Data;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

// using Unity.Mathematics;
// Unity.Mathematics.Random;

namespace Authoring
{
    public class ArenaAuthoring : MonoBehaviour
    {
        public LevelDataSO LevelData;
        [field: SerializeField] public GameObject FieldPrefab { get; private set; }
        
        [field: SerializeField] public int RandomSeed { get; private set; }
        
        public int LevelSize => LevelData.LevelSize;
        public int TurretNumber => LevelData.TurretNumber;

        public GameObject TurretPrefab => LevelData.TurretPrefab;
    }
    
    public class ArenaBaker : Baker<ArenaAuthoring>
    {
        public override void Bake(ArenaAuthoring authoring)
        {

            DependsOn(authoring.TurretPrefab);
            
            if (authoring.TurretPrefab == null) return;
            
            Entity entity = this.GetEntity(TransformUsageFlags.Dynamic);
            var randomSeed = Random.Range(1, 10000);
            
            
            AddComponent(entity, new ArenaProperties
            {
                LevelSize = authoring.LevelSize,
                TurretNumber = authoring.TurretNumber,
                FieldPrefab = GetEntity(authoring.FieldPrefab, TransformUsageFlags.WorldSpace),
                TurretPrefab = GetEntity(authoring.LevelData.TurretPrefab, TransformUsageFlags.WorldSpace),
            });
            
            AddComponent(entity, new ArenaRandom
            {
                Value = Unity.Mathematics.Random.CreateFromIndex((uint)randomSeed)
            });
            
        }
    }
}