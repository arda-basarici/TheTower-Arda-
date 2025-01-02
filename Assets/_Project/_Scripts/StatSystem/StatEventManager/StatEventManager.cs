namespace Game
{
    public class StatEventManager : EventManager<StatState>
    {
        private StatState currentState;

        public StatEventManager()
        {
            currentState = new StatState { CurrentValue = 0 };
        }

        public void UpdateStat(StatType type, float currentValue)
        {
            currentState.CurrentValue = currentValue;
            currentState.StatType = type;
            NotifyStateObservers(currentState);
        }
    }
}
