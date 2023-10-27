using Unity.Scenes;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Level.Data
{
    [CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/LevelDataSO/New Level Data")]
    public class LevelDataSO : ScriptableObject
    {
        [field: SerializeField] public Color TurretColor { get; private set; }
        [field: SerializeField] public int TurretNumber { get; private set; }

        [field: SerializeField]
        [field: Range(1, 100)]
        public int LevelSize { get; private set; } = 1;
        [field: SerializeField] public GameObject TurretPrefab { get; private set; }
        
        [field: SerializeField] public SceneAsset Scene { get; private set; }
        
        [field: SerializeField] public float CameraOrthographicSize { get; private set; }
    }
}