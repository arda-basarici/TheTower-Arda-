namespace Game
{
    public interface IGamePlayStateEnterListener : IGameStateListener
    {
        void OnGamePlayStateEnter();
    }

    public interface IGamePlayStateExitListener : IGameStateListener
    {
        void OnGamePlayStateExit();
    }

    public interface IGamePlayStatePlayingUpdateListener : IGameStateListener 
    {
        void GamePlayStatePlayingUpdate();
    }

    public interface IGamePlayStatePausedUpdateListener : IGameStateListener
    {
        void GamePlayStatePausedUpdate();
    }

    public interface IGamePlayStateResumeListener : IGameStateListener
    {
        void OnGamePlayStateResume();
    }

    public interface IGamePlayStatePauseListener : IGameStateListener
    {
        void OnGamePlayStatePause();
    }
}