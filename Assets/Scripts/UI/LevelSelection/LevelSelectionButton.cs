using System;
using EventArgs;
using Level.Data;
using UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelSelection
{
    public class LevelSelectionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private ButtonLevelSelectionSO _levelSelectionSO;

        public LevelDataSO LevelData { get; set; }

        public int ButtonId { get; set; }
        public event Action<ButtonClickedEventArgs> LevelSelectedButtonClicked; 
        private void Awake()
        {
            _buttonImage.sprite = _levelSelectionSO.ButtonBackground;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            LevelSelectedButtonClicked?.Invoke(new ButtonClickedEventArgs
            {
                ButtonId = ButtonId
            });
        }

        public void SetActive(bool isActive)
        {
            _buttonImage.sprite = isActive ? _levelSelectionSO.ActiveButtonBackground : _levelSelectionSO.ButtonBackground;
        }
    }
}