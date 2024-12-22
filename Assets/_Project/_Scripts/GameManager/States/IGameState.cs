using Game;

public interface IGameState
{
    GameStateType Type { get; }
    void OnEnter();
    void OnExit();
    void Update();
}