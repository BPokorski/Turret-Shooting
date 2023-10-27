using System;
using EventArgs;
using SceneLoader.Data;
using Systems;
using Unity.Entities;
using UnityEngine;

namespace GameManagers
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private ScenePathSO _gameOverScene;
        [SerializeField] private ScenePathSO _thisScene;
        private void OnEnable()
        {
            var countLeftTurretSystem =
                World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<GameOverSystem>();

            countLeftTurretSystem.Enabled = true;
            countLeftTurretSystem.OnGameOver += InvokeGameOver;
        }

        private void OnDisable()
        {
            if (World.DefaultGameObjectInjectionWorld == null) return;
            var countLeftTurretSystem =
                World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<GameOverSystem>();

            countLeftTurretSystem.OnGameOver -= InvokeGameOver;
        }

        private void InvokeGameOver(GameOverArgs gameOverArgs)
        {
            SceneManagement.SceneLoader.LoadScene(_gameOverScene, true);
            SceneManagement.SceneLoader.ChangeActiveScene(_gameOverScene.ScenePath);
            SceneManagement.SceneLoader.UnLoadScene(_thisScene);
        }
    }
}