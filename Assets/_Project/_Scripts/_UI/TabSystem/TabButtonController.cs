using UnityEngine;
using UnityEngine.Rendering;
namespace Game
{
    public class TabButtonController : ButtonController
    {
        [SerializeField] private TabPanel tabToView;
        [SerializeField] private TabGroups tabGroup;

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
            button.image.color = Color.green;   
        }

        private void UnhighlightButton()
        {
            button.image.color = Color.white;
        }
    }
}