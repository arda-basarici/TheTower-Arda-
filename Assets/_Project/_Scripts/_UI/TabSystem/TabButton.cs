using UnityEngine;

namespace Game
{
    public class TabButton : MonoBehaviour
    {
        
        [SerializeField] private TabPanel _tabToOpen;
        private void Awake()
        {
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            Debug.Log("TabButton.OnClick");
            TabSystem.Show(_tabToOpen);
        }
    }
}