using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class TabManager : MonoBehaviour
    {
        [SerializeField] private TabGroups id;
        [SerializeField] public List<TabPanel> tabPanels;
        [SerializeField] private TabPanel InitialTab; 
        private TabPanel _currentTab;

        private void Awake()
        {
            TabSystem.RegisterTabManager(id,this);
        }

        private void OnDestroy()
        {
            TabSystem.UnregisterTabManager(id);
        }

        public void Show<T>() where T : TabPanel
        {
            foreach (var tabPanel in tabPanels)
            {
                if (tabPanel is T view)
                {
                    Show(view);
                    return;
                }
            }
        }

        public void Show(TabPanel tabPanel)
        {
            if (_currentTab != null)
            {
                _currentTab.Hide();
            }
            _currentTab = tabPanel;
            _currentTab.Show();
        }

        public void Hide(TabPanel tabPanel)
        {
            if (_currentTab != null)
            {
                _currentTab.Hide();
            }
        }

        public void Hide<T>() where T : TabPanel
        {
            foreach (var tabPanel in tabPanels)
            {
                if (tabPanel is T view)
                {
                    Hide(view);
                    return;
                }
            }
        }

        private void HideAll()
        {
            foreach (var tabPanel in tabPanels)
            {
                tabPanel.Hide();
            }
        }

        public T GetView<T>() where T : TabView
        {
            for (int i = 0; i < tabPanels.Count; i++)
            {
                if (tabPanels[i] is T view)
                {
                    return view;
                }
            }
            return null;
        }



        private void Start()
        {
            foreach (var tabPanel in tabPanels)
            {
                tabPanel.Initialize();
            }
            HideAll();
            Show(InitialTab);
        }
    }
}
