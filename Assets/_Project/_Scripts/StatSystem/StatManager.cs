using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class StatManager : MonoBehaviour
    {
        [SerializeField]
        private StatData statData;
        private readonly Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();

        protected void Awake()
        {
            InitializeStats();
        }

        private void InitializeStats()
        {
            if (statData != null)
            {
                foreach (StatType statType in System.Enum.GetValues(typeof(StatType)))
                {
                    Stat stat = StatFactory.GetStat(statType, statData.GetBaseValue(statType), GetComponent<Identifier>().ID);
                    if (stat != null)
                    {
                        stats.Add(statType, stat);
                    }
                }
            }
        }
        private Stat GetStat(StatType statType)
        {
            if (stats.TryGetValue(statType, out Stat stat))
            {
                return stat;
            }
            Debug.LogWarning($"Stat '{statType}' not found on {gameObject.name}");
            return null;
        }

        public void IncreaseStat(StatType statType, float value)
        {
            Stat stat = GetStat(statType);
            stat?.IncreaseStat(value);
        }

        public void DecreaseStat(StatType statType, float value)
        {
            Stat stat = GetStat(statType);
            stat?.DecreaseStat(value);
        }

        public void SetStat(StatType statType, float value)
        {
            Stat stat = GetStat(statType);
            stat.CurrentValue = value;  
            
        }

        public float GetCurrentValue(StatType statType)
        {
            return GetStat(statType).CurrentValue;
        }
    }
}