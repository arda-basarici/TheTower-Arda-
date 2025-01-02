using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
    public static class StatFactory
    {
        private static Dictionary<StatType, Type> statDictionary;

        public static bool IsInitialized => statDictionary != null;

        private static void Initialize()
        {
            if (IsInitialized)
                return;

            statDictionary = new Dictionary<StatType, Type>();

            var stats = Assembly.GetAssembly(typeof(Stat))
                                .GetTypes()
                                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Stat)));

            foreach (var stat in stats)
            {
                var tempInstance = Activator.CreateInstance(stat) as Stat;
                statDictionary.Add(tempInstance.Type, stat);
            }
        }

        public static Stat GetStat(StatType statType, float baseValue, string gameobjId)
        {
            Initialize();

            if (statDictionary.TryGetValue(statType, out var statTypeClass))
            {
                return Activator.CreateInstance(statTypeClass, new object[] { baseValue, gameobjId }) as Stat;
            }

            return null;
        }
    }
}
