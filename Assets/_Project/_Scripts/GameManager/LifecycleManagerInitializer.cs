namespace Game
{
    public class LifecycleManagerInitializer : IInitializable
    {
        public InitializationPhase Phase => InitializationPhase.Global;
        public void Initialize()
        {
            LifecycleManager.Initialize();
        }
    }
}