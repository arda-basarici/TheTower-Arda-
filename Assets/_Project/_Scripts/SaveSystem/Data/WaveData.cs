using System;
using JetBrains.Annotations;

namespace Game
{
    [Serializable]
    public class WaveSystemData : IData
    {
        public int currentWave = 1;

        public const int currentVersion = Versions.WaveSystemData;
        public int Version { get; set; } = currentVersion;

        public WaveSystemData()
        {
            Version = Versions.WaveSystemData;
            currentWave = 1;
        }
    }
}