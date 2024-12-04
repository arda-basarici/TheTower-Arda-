namespace Game
{
    public interface IGameStateObserver
    {
        void OnStateChange(IGameState state);
    }
}