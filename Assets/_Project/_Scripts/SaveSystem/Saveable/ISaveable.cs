
namespace Game
{
    public interface ISaveable
    {
        int Version{ get; set; }

        void Save();

        void Load();
    }
}