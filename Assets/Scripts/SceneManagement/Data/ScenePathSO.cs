using UnityEngine;

namespace SceneLoader.Data
{
    [CreateAssetMenu(fileName = "New Scene Path", menuName = "ScriptableObjects/Scenes/ScenePath")]
    public class ScenePathSO : ScriptableObject
    {
        [field: SerializeField] public string ScenePath { get; private set; }
    }
}