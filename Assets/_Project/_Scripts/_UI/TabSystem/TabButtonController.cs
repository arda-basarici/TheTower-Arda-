using UnityEngine;
namespace Game
{
    public class TabButtonController : ButtonController
    {
        [SerializeField] private TabPanel tabToView;
        [SerializeField] private TabGroups tabGroup;
        [SerializeField] private GameObject focus;

        protected override void Awake()
        {
            base.Awake();
            TabSystem.SubscribeToTabGroup(tabGroup, HandleTabChange);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TabSystem.UnsubscribeFromTabGroup(tabGroup, HandleTabChange);
        }
        

        protected override void OnClick()
        {
            TabSystem.Show(tabToView);
        }

        private void HandleTabChange(TabPanel currentTab)
        {
            if(currentTab == tabToView)
            {
                HighlightButton();
            }
            else
            {
                UnhighlightButton();
            }
        }

        private void HighlightButton()
        {
            if (focus == null) return;
            focus.SetActive(true);
        }

        private void UnhighlightButton()
        {
            if (focus == null) return;
            focus.SetActive(false);
        }
    }
}