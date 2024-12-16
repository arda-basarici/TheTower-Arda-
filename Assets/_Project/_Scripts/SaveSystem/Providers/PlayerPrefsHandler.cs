using UnityEngine;

namespace Game
{
    public class PlayerPrefsHandler : ISaveHandler
    {
        public void Save(string key, object data)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public T Load<T>(string key)
        {
            return PlayerPrefs.HasKey(key) ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(key)) : default;
        }

        public bool Exists(string key) => PlayerPrefs.HasKey(key);
        public void Delete(string key) => PlayerPrefs.DeleteKey(key);
    }
}