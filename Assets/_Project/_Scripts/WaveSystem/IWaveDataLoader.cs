using System.Collections.Generic;

namespace Game
{
    public interface IWaveDataLoader
    {
        List<WaveData> LoadWaveData();
    }
}