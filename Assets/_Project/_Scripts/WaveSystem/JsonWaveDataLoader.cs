using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class JsonWaveDataLoader : IWaveDataLoader
    {

        public List<WaveData> LoadWaveData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(ResourcePaths.WaveData);
            if (jsonFile == null)
            {
                Debug.LogError("Json file not found");
                return null;
            }
            return JsonUtility.FromJson<List<WaveData>>(jsonFile.text);
        }

    }
}