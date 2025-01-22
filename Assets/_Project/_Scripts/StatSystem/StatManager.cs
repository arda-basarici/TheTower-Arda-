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
            else
            {
                Stat s = StatFactory.GetStat(statType, 0, GetComponent<Identifier>().ID);
                if (s != null)
                {
                    stats.Add(statType, s);
                }
                return s;
            }
            
            
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
            stat.SetStat(value);

        }

        public float GetCurrentValue(StatType statType)
        {
            return GetStat(statType).GetValue();
        }
    }
}