using EventArgs;
using Level.Data;
using SceneLoader.Data;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Buttons.Navigation
{
    public class StartGameButton : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Image _padIcon;
        [SerializeField] private ActiveLevelSO _activeLevel;
        [SerializeField] private Sprite _enabledButtonBackground;

        [SerializeField] private ScenePathSO _gameplayScene;
        [SerializeField] private ScenePathSO _thisScene;
        private void Awake()
        {
            _padIcon.enabled = false;
            _startButton.interactable = false;
        }
        
        private void OnEnable()
        {
            _activeLevel.ActiveLevelChanged += OnActiveLevelChanged;
            // _startButton.OnPointerClick();
            _startButton.onClick.AddListener(StartGame);
            SceneManagement.SceneLoader.LoadScene(_gameplayScene, true);
        }

        private void OnDisable()
        {
            _activeLevel.ActiveLevelChanged -= OnActiveLevelChanged;
            _startButton.onClick.RemoveListener(StartGame);
            
            _padIcon.enabled = false;
            _startButton.interactable = false;
        }

        private void OnActiveLevelChanged(ChangeActiveLevelArgs eventArgs)
        {
            if (eventArgs.ActiveLevelData == null) return;
            _padIcon.enabled = true;
            _startButton.interactable = true;
            _startButton.GetComponent<Image>().sprite = _enabledButtonBackground;
        }

        private void StartGame()
        {
            
            SceneManagement.SceneLoader.ChangeActiveScene(_gameplayScene.ScenePath);
            SubSceneLoader.Instance.LoadLevel();
            SceneManagement.SceneLoader.UnLoadScene(_thisScene);
        }
    }
}