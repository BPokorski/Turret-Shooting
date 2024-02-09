using EventArgs;
using Systems;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace UI.TurretCounter
{
    public class UITurretCounter : MonoBehaviour
    {
        [SerializeField] private GameObject _turretCountPanel;
        [SerializeField] private TextMeshProUGUI _turretNumber;

        private void OnEnable()
        {
            var turretCounterSystem =
                World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<TurretCounterSystem>();
            
            turretCounterSystem.TurretNumberChanged += OnTurretNumberChange;
        }
        
        private void OnDisable()
        {
            if (World.DefaultGameObjectInjectionWorld == null) return;
            var countTurretLeftSystem =
                World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<TurretCounterSystem>();

            countTurretLeftSystem.TurretNumberChanged -= OnTurretNumberChange;
            _turretCountPanel.SetActive(false);
        }

        private void OnTurretNumberChange(IntEventArgs turretNumberEventArgs)
        {
            _turretNumber.text = turretNumberEventArgs.Value.ToString();
        }

        public void EnableTurretCounting()
        {
            _turretCountPanel.SetActive(true);
        }
    }
}