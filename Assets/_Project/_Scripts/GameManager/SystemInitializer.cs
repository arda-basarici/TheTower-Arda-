using System.Collections.Generic;

namespace Game
{
    public static class SystemInitializer
    {
        public static void InitializeSystemsByPhase(InitializationPhase phase)
        {
            List<IInitializable> systems = GameFactory.GetInitializersByPhase(phase);

            foreach (var system in systems)
            {
                system.Initialize();
            }
        }
    }
}
