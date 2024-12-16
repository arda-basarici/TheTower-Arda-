using System;

namespace Game
{
    [Serializable]
    public class GameMetaData : IData
    {
        public const int currentVersion = Versions.GameMetaData;
        public int Version { get; set; } = currentVersion;
        public string saveTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string gameVersion = Versions.GameVersion;
        
        GameMetaData()
        {
            Version = Versions.GameMetaData;
            saveTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            gameVersion = Versions.GameVersion;
        }

    }
}