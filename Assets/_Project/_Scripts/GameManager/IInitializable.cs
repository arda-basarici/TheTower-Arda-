namespace Game
{
    public interface IInitializable
    {
        InitializationPhase Phase { get; }
        void Initialize();
    }
}