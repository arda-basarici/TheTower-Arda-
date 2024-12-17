using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
    public static class GameFactory
    {
        private static Dictionary<string, Type> initializerDictionary;

        private static bool IsInitialized => initializerDictionary != null;

        private static void Initialize()
        {
            if (IsInitialized)
                return;

           
            var initializers = Assembly.GetAssembly(typeof(IInitializable))
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IInitializable).IsAssignableFrom(t));

            initializerDictionary = new Dictionary<string, Type>();

            foreach (var initializer in initializers)
            {
                initializerDictionary.Add(initializer.Name, initializer);
            }
        }

        public static List<IInitializable> GetInitializersByPhase(InitializationPhase phase)
        {
            Initialize();

            return initializerDictionary.Values
                .Select(type => Activator.CreateInstance(type) as IInitializable)
                .Where(initializer => initializer != null && initializer.Phase == phase)
                .ToList();
        }
    }
}
