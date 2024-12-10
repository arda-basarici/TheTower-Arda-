using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
    public class ScriptableObjectsWaveDataLoader : IWaveDataLoader
    {
        public List<WaveData> LoadWaveDataAndReturn()
        {
            return Resources.LoadAll<WaveData>(ResourcePaths.WaveData).ToList();
        }
    }

}