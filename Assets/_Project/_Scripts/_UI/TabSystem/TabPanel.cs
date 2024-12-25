using UnityEngine;

namespace Game
{
    public abstract class TabPanel : MonoBehaviour
    {
        public abstract void Initialize();
        public virtual void Hide() => gameObject.SetActive(false);
        public virtual void Show() => gameObject.SetActive(true);
    }
}
