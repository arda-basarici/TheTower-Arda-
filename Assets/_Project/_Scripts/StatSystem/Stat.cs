using UnityEditor.Build.Content;

namespace Game
{
    public abstract class Stat
    {
        public abstract StatType Type { get; }
        public StatStateManager statEventManager;
        private StatState statState;
        protected string objectId;

        protected Stat(float baseValue, string id)
        {
            statState.CurrentValue = baseValue;
            objectId = id;
            statEventManager = StateSystem.Get<StatStateManager>(Type.ToString() + id);
            statEventManager.UpdateStat(statState);
        }

        public virtual void IncreaseStat(float value)
        {
            statState.CurrentValue += value;
            statEventManager.UpdateStat(statState);
        }
        public virtual void DecreaseStat(float value)
        {
            statState.CurrentValue -= value;
            statEventManager.UpdateStat(statState);
        }

        public virtual void SetStat(float value)
        {
            statState.CurrentValue = value;
            statEventManager.UpdateStat(statState);
        }

        public virtual float GetValue()
        {
            return statState.CurrentValue;
        }
    }
}