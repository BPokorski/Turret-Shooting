using System;
using EventArgs;
using Level.Data;
using UnityEngine;

namespace CameraManagement
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private ActiveLevelSO _activeLevel;
        [SerializeField] private Camera _mainCamera;

        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = transform;
        }

        private void OnEnable()
        {
            _activeLevel.ActiveLevelChanged += ChangeCameraPosition;
        }

        private void OnDisable()
        {
            _activeLevel.ActiveLevelChanged -= ChangeCameraPosition;
        }

        private void ChangeCameraPosition(ChangeActiveLevelArgs newActiveLevelArgs)
        {
            var selectedLevel = newActiveLevelArgs.ActiveLevelData;
            
            _cameraTransform.position = new Vector3(selectedLevel.LevelSize / 2f -0.5f, selectedLevel.LevelSize / 2f,
                selectedLevel.LevelSize / 2f);

            _mainCamera.orthographicSize = selectedLevel.CameraOrthographicSize;
        }
    }
}