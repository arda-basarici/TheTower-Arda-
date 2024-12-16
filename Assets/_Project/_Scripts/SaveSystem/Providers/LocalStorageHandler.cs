using System.Runtime.InteropServices;
using Game;
using UnityEngine;

public class LocalStorageHandler : ISaveHandler
{
    [DllImport("__Internal")] private static extern void SetLocalStorage(string key, string value);
    [DllImport("__Internal")] private static extern string GetLocalStorage(string key);

    public void Save(string key, object data)
    {
        SetLocalStorage(key, JsonUtility.ToJson(data));
    }

    public T Load<T>(string key)
    {
        string json = GetLocalStorage(key);
        return string.IsNullOrEmpty(json) ? default : JsonUtility.FromJson<T>(json);
    }

    public bool Exists(string key) => !string.IsNullOrEmpty(GetLocalStorage(key));
    public void Delete(string key) => SetLocalStorage(key, "");
}
