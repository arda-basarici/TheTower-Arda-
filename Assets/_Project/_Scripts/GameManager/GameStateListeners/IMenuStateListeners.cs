namespace Game
{
    public interface IMenuStateEnterListener : IGameStateListener
    {
        void OnMenuStateEnter();
    }

    public interface IMenuStateExitListener : IGameStateListener
    {
        void OnMenuStateExit();
    }

    public interface IMenuStateUpdateListener : IGameStateListener
    {
        void MenuStateUpdate();
    }
}