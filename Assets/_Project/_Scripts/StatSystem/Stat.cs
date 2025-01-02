namespace Game
{
    public abstract class Stat
    {
        public abstract StatType Type { get; }
        public float BaseValue { get; protected set; }
        public float CurrentValue;
        public StatEventManager statEventManager;


        protected Stat(float baseValue, string id)
        {
            BaseValue = baseValue;
            CurrentValue = baseValue;
            statEventManager = EventSystem.Get<StatEventManager>(Type.ToString() + id);
            statEventManager.UpdateStat(Type,CurrentValue);
        }
        public virtual void IncreaseStat(float value)
        {
            CurrentValue += value;
            statEventManager.UpdateStat(Type,CurrentValue);
        }
        public virtual void DecreaseStat(float value)
        {
            CurrentValue -= value;
            statEventManager.UpdateStat(Type, CurrentValue);
        }

        public virtual void SetStat(float value)
        {
            CurrentValue = value;
            statEventManager.UpdateStat(Type, CurrentValue);
        }
    }
}