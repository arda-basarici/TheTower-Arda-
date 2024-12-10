namespace Game
{
    public interface IStatObserver
    {
        void OnStatChange(StatType type,float value);
    }
}