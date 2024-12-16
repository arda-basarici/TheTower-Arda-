using System;

namespace Game
{
    [Serializable]
    public class GameMetaData
    {
        public string saveTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string gameVersion = "1.0.0";
    }
}