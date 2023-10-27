using System;
using SceneLoader.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Navigation
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _mainMenuButton;

        [SerializeField] private ScenePathSO _levelSelectionScene;
        [SerializeField] private ScenePathSO _thisScene;

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(NavigateToLevelSelection);
            
        }
        
        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(NavigateToLevelSelection);
        }

        private void NavigateToLevelSelection()
        {
            SceneManagement.SceneLoader.LoadScene(_levelSelectionScene, true);
            SceneManagement.SceneLoader.ChangeActiveScene(_levelSelectionScene.ScenePath);
            SceneManagement.SceneLoader.UnLoadScene(_thisScene);

        }
    }
}