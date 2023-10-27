using SceneLoader.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public static class SceneLoader
    {
        public static void LoadScene(string scenePath, bool additive)
        {
            if (IsSceneLoaded(scenePath)) return;

            SceneManager.LoadScene(scenePath, additive? LoadSceneMode.Additive: LoadSceneMode.Single);
        }

        public static void LoadScene(ScenePathSO scenePathSO, bool additive)
        {
            var scenePath = scenePathSO.ScenePath;
            if (IsSceneLoaded(scenePath)) return;

            SceneManager.LoadScene(scenePath, additive? LoadSceneMode.Additive: LoadSceneMode.Single);
        }

        
        public static AsyncOperation UnLoadScene(string scenePath)
        {
            if (!IsSceneLoaded(scenePath)) return null;

            return SceneManager.UnloadSceneAsync(scenePath);
        }

        public static AsyncOperation UnLoadScene(ScenePathSO scenePathSO)
        {
            var scenePath = scenePathSO.ScenePath;
            if (!IsSceneLoaded(scenePath)) return null;

            return SceneManager.UnloadSceneAsync(scenePath);
        }
        
        private static bool IsSceneLoaded(string scenePath)
        {
            var scene = SceneManager.GetSceneByPath(scenePath);
            return scene.isLoaded;
        }

        public static void ChangeActiveScene(Scene scene)
        {
            if (!scene.isLoaded) return;
            SceneManager.SetActiveScene(scene);
        }
        
        public static void ChangeActiveScene(string scenePath)
        {
            var scene = SceneManager.GetSceneByPath(scenePath);
            
            if (!scene.isLoaded || !scene.IsValid()) return;
            SceneManager.SetActiveScene(scene);
        }
        
    }
}