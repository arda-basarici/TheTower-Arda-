namespace Game
{
    public interface ISaveHandler
    {
        void Save(string key, object data);
        T Load<T>(string key);
        bool Exists(string key);
        void Delete(string key);
    }
}