using UnityEngine;

namespace UI.Data
{
    [CreateAssetMenu(fileName = "New Level Selection Data", menuName = "ScriptableObjects/Button/New Level Selection Data")]
    public class ButtonLevelSelectionSO : ScriptableObject
    {
        [field: SerializeField] public Sprite ButtonBackground { get; private set; }
        [field: SerializeField] public Sprite ActiveButtonBackground { get; private set; }
        [field: SerializeField] public GameObject ButtonPrefab { get; private set; }
    }
}