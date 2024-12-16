using Game;

public static class SaveHandlerFactory
{
    public static ISaveHandler CreateSaveHandler()
    {
#if UNITY_WEBGL
        return new LocalStorageHandler();
#elif UNITY_EDITOR || UNITY_STANDALONE
        return new JsonFileHandler();
#elif UNITY_ANDROID || UNITY_IOS
        return new PlayerPrefsHandler();
#else
        Debug.LogWarning("No save handler for this platform. Defaulting to PlayerPrefsHandler.");
        return new PlayerPrefsHandler();
#endif
    }
}
