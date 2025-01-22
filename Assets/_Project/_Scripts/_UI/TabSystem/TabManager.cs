using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class TabManager : MonoBehaviour
    {
        
        [SerializeField] private TabGroups id;
        [SerializeField] public List<TabPanel> tabPanels;
        [SerializeField] public TabPanel InitialTab;
        [SerializeField] public GameObject panelTitle; 
        private TabPanel _currentTab;
        protected void Awake()
        {
            TabSystem.RegisterTabManager(id, this);

        }

        protected void Start()
        {
           Initialize();
        }

        protected void OnDestroy()
        {
            TabSystem.UnregisterTabManager(id);
        }

        public void Initialize()
        {
            foreach (var tabPanel in tabPanels)
            {
                if (tabPanel != null)
                {
                    tabPanel.Initialize();
                }

            }
            HideAll();
            Show(InitialTab);
            TabSystem._tabGroupEvents[id]?.Invoke(InitialTab);
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
            SetPanelTitle();
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
                if(tabPanel != null)
                {
                    tabPanel.Hide();
                }
                    
            }
        }

        public T GetPanel<T>() where T : TabView
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

        public void SetPanelTitle()
        {
            if (panelTitle == null || panelTitle.GetComponent<TextMeshProUGUI>() == null) return;
            
            panelTitle.GetComponent<TextMeshProUGUI>().text = _currentTab.Title;
        }
    }
}
