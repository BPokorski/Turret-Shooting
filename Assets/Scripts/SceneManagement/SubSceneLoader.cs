using Level.Data;
using Systems;
using Unity.Entities;
using Unity.Entities.Serialization;
using Unity.Scenes;
using UnityEngine;

namespace SceneManagement
{
    public class SubSceneLoader : MonoBehaviour
    {
        public static SubSceneLoader Instance { get; private set; }
        
        [SerializeField] private ActiveLevelSO _activeLevel;
        
        private EntitySceneReference _sceneReference;
        private Entity _sceneEntity;
        private void Awake()
        {
            Instance = this;
        }

        
        private void OnDisable()
        {
            SceneSystem.UnloadScene(World.DefaultGameObjectInjectionWorld.Unmanaged, _sceneEntity);
        }

        public void LoadLevel()
        {
            
            var selectedLevel = _activeLevel.SelectedLevel;
            
            if (selectedLevel == null) return;
            _sceneReference = new EntitySceneReference(selectedLevel.Scene);
            _sceneEntity = SceneSystem.LoadSceneAsync(World.DefaultGameObjectInjectionWorld.Unmanaged, _sceneReference);
            // var camera = Camera.main;
            // var cameraPosition = camera.gameObject.transform.position;
            // camera.gameObject.transform.position = new Vector3(selectedLevel.LevelSize / 2f, selectedLevel.LevelSize / 2f,
            //     selectedLevel.LevelSize / 2f);
            //
            // camera.orthographicSize = 10f;
            World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<GameOverSystem>().Enabled = true;
            World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<SpawnFieldsSystem>().Enabled = true;
            World.DefaultGameObjectInjectionWorld.Unmanaged.GetExistingSystemState<InitTurretSystem>().Enabled = true;
            
        }
    }
}