using System;
using EventArgs;
using UnityEngine;

namespace Level.Data
{
    [CreateAssetMenu(fileName = "Active Level", menuName = "ScriptableObjects/Level/Active Level")]
    public class ActiveLevelSO : ScriptableObject
    {
        [field: SerializeField] public LevelDataSO SelectedLevel { get; private set; }

        public event Action<ChangeActiveLevelArgs> ActiveLevelChanged;


        public void ChangeActiveLevel(LevelDataSO newActiveLevel)
        {
            SelectedLevel = newActiveLevel;
            var activeLevelArgs = new ChangeActiveLevelArgs
            {
                ActiveLevelData = SelectedLevel
            };
            ActiveLevelChanged?.Invoke(activeLevelArgs);
        }

    }
}