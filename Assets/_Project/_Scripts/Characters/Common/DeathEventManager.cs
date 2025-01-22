
namespace Game
{
    public class DeathEventManager : EventManager
    {
        public void NotifyDeathEvent()
        {
            NotifyEventObservers();
        }

    }
}