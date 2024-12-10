using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game
{
    [CreateAssetMenu(fileName = "StatData", menuName = "Scriptable Objects/StatData")]
    public class StatData : SerializedScriptableObject
    {
        [ShowInInspector, TableList(AlwaysExpanded = true)]
        public SerializedDictionary<StatType, float> baseValues = new SerializedDictionary<StatType, float>();

        public float GetBaseValue(StatType statType)
        {
            if (baseValues.TryGetValue(statType, out float value))
            {
                return value;
            }
            return 0f;
        }

        [Button(ButtonSizes.Large, Name = "Add Stat")]
        private void AddStat(StatType statType, float value)
        {
            if (!baseValues.ContainsKey(statType))
            {
                baseValues.Add(statType, value);
            }
            else
            {
                Debug.LogWarning($"Stat {statType} already exists in the dictionary.");
            }
        }

    }
}