using UnityEngine;

namespace Game
{
    public class HealthStat : Stat
    {
        public override StatType Type => StatType.Health;

        public HealthStat() : base(0, "") { }
        public HealthStat(float baseValue, string id) : base(baseValue, id) { }

        public override void DecreaseStat(float value)
        {
            base.DecreaseStat(value);
            if (GetValue() <= 0)
            {
                EventSystem.Get<DeathEventManager>( EventManagerId.death + objectId).NotifyDeathEvent();
            }
        }
    }
}