using System.Collections.Generic;
using EventArgs;
using Level.Data;
using UI.Data;
using UnityEngine;

namespace UI.LevelSelection
{
    public class LevelSelectionManager : MonoBehaviour
    {
        [SerializeField] private List<LevelDataSO> _levels = new List<LevelDataSO>();
        [SerializeField] private Transform _levelSelectionContainer;
        [SerializeField] private ButtonLevelSelectionSO _levelSelectionSO;
        [SerializeField] private float _buttonPositionOffset;
        [SerializeField] private ActiveLevelSO _activeLevel;
        
        private List<LevelSelectionButton> _selectionButtons = new List<LevelSelectionButton>();

        private void Awake()
        {
            var yPositionOffset = 0.0f;
            int buttonId = 0;
            foreach (var level in _levels)
            {
                var levelSelectionButtonGO = Instantiate(_levelSelectionSO.ButtonPrefab, _levelSelectionContainer);

                var buttonRectTransform = (RectTransform) levelSelectionButtonGO.transform;
                buttonRectTransform.anchoredPosition = new Vector2(0.0f, buttonRectTransform.rect.y + yPositionOffset);

                var levelSectionButton = levelSelectionButtonGO.GetComponent<LevelSelectionButton>();
                var levelSelectionView = levelSelectionButtonGO.GetComponent<LevelSelectionView>();

                levelSectionButton.LevelData = level;
                levelSelectionView.LevelData = level;
                levelSectionButton.ButtonId = buttonId;
                _selectionButtons.Add(levelSectionButton);

                yPositionOffset += _buttonPositionOffset;
                buttonId++;
            }
            
        }

        private void OnEnable()
        {
            foreach (var selectionButton in _selectionButtons)
            {
                selectionButton.LevelSelectedButtonClicked += OnLevelSelectionChanged;
            }
        }

        private void OnDisable()
        {
            foreach (var selectionButton in _selectionButtons)
            {
                selectionButton.LevelSelectedButtonClicked -= OnLevelSelectionChanged;
            }
        }

        private void OnLevelSelectionChanged(ButtonClickedEventArgs eventArgs)
        {
            int buttonId = eventArgs.ButtonId;
            foreach (var selectionButton in _selectionButtons)
            {
                selectionButton.SetActive(selectionButton.ButtonId == buttonId);
            }
            _activeLevel.ChangeActiveLevel(_levels[buttonId]);
        }
    }
}