using Level.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.LevelSelection
{
    public class LevelSelectionView : MonoBehaviour
    {
        
        [SerializeField] private Image _turretIcon;
        [SerializeField] private TextMeshProUGUI _turretNumber;
        
        public LevelDataSO LevelData { get; set; }
        private void Start()
        {
            _turretIcon.color = LevelData.TurretColor;
            _turretNumber.text = LevelData.TurretNumber.ToString();
        }
    }
}