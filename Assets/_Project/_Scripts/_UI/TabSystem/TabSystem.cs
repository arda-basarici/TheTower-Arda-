using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class TabSystem
    {
        private static readonly Dictionary<TabGroups, TabManager> _tabManagers = new Dictionary<TabGroups, TabManager>();
        public static void RegisterTabManager(TabGroups tabManager, TabManager tabManagerInstance)
        {
            if (!_tabManagers.ContainsKey(tabManager))
            {
                _tabManagers.Add(tabManager, tabManagerInstance);
            }
        }

        public static void UnregisterTabManager(TabGroups tabManager)
        {
            if (_tabManagers.ContainsKey(tabManager))
            {
                _tabManagers.Remove(tabManager);
            }
        }

        public static TabManager GetTabManager(TabGroups tabManager)
        {
            if (_tabManagers.ContainsKey(tabManager))
            {
                return _tabManagers[tabManager];
            }
            return null;
        }

        public static void Show<T>() where T : TabPanel
        {
            foreach (var tabManager in _tabManagers)
            {
                foreach (var tabPanel in tabManager.Value.tabPanels)
                {
                    if (tabPanel is T view)
                    {
                        tabManager.Value.Show(view);
                        return;
                    }
                }
            }

        }

        public static void Show(TabPanel tabPanel)
        {
            Debug.Log("TabSystem.Show");
            foreach (var tabManager in _tabManagers)
            {
                if(tabManager.Value.tabPanels.Contains(tabPanel))
                {
                    tabManager.Value.Show(tabPanel);
                    return;
                }
            }
        }
    }
}