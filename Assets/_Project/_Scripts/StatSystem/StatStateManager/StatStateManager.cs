namespace Game
{
    public class StatStateManager : StateManager<StatState>
    {
        private StatState currentState;

        public StatStateManager()
        {
            currentState = new StatState { CurrentValue = 0 };
        }

        public void UpdateStat(StatState statState)
        {
            currentState.CurrentValue = statState.CurrentValue;
            currentState.StatType = statState.StatType;
            NotifyStateObservers(currentState);
        }
    }
}
