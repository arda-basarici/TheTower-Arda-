using System.IO;
using UnityEngine;

namespace Game
{
    public class JsonFileHandler : ISaveHandler
    {
        private string basePath = Application.persistentDataPath;

        public void Save(string key, object data)
        {
            string path = Path.Combine(basePath, $"{key}.json");
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }

        public T Load<T>(string key)
        {
            string path = Path.Combine(basePath, $"{key}.json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(json);
            }
            return default;
        }

        public bool Exists(string key) => File.Exists(Path.Combine(basePath, $"{key}.json"));

        public void Delete(string key)
        {
            string path = Path.Combine(basePath, $"{key}.json");
            if (File.Exists(path)) File.Delete(path);
        }
    }
}