using UnityEngine;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;

namespace Game
{
    public class StatManager : MonoBehaviour
    {
        [SerializeField]
        private StatData statData;

        private Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();

        private void Awake()
        {
            InitializeStats();
        }

        private void InitializeStats()
        {   
            //Debug.Log("Initializing stats");
            if (statData != null)
            {
                stats.Add(StatType.Health, new HealthStat(statData.health));
                stats.Add(StatType.Damage, new DamageStat(statData.damage));
                stats.Add(StatType.FireRate, new FireRateStat(statData.fireRate));
                stats.Add(StatType.Speed, new SpeedStat(statData.speed));
            }
        }
        public Stat GetStat(StatType statType)
        {
            if (stats.TryGetValue(statType, out Stat stat))
            {
                return stat;
            }
            Debug.LogWarning($"Stat '{statType}' not found on {gameObject.name}");
            return null;
        }

        public void RegisterObserver(StatType statType, IStatObserver observer)
        {
            Stat stat = GetStat(statType);
            stat?.RegisterObserver(observer);
        }

        public void UnregisterObserver(StatType statType, IStatObserver observer)
        {
            Stat stat = GetStat(statType);
            stat?.UnregisterObserver(observer);
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
    }
}