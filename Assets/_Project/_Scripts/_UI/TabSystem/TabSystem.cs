using System;
using System.Collections.Generic;

namespace Game
{
    public static class TabSystem
    {
        private static readonly Dictionary<TabGroups, TabManager> _tabManagers = new Dictionary<TabGroups, TabManager>();
        public static readonly Dictionary<TabGroups, Action<TabPanel>> _tabGroupEvents = new Dictionary<TabGroups, Action<TabPanel>>();
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

        public static T GetPanel<T>() where T : TabPanel
        {
            foreach (var tabManager in _tabManagers)
            {
                foreach (var tabPanel in tabManager.Value.tabPanels)
                {
                    if (tabPanel is T view)
                    {
                        
                        return view;
                    }
}
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
                        _tabGroupEvents[tabManager.Key]?.Invoke(view);
                        return;
                    }
                }
            }

        }

        public static void Show(TabPanel tabPanel)
        {
            foreach (var tabManager in _tabManagers)
            {
                if(tabManager.Value.tabPanels.Contains(tabPanel))
                {
                    tabManager.Value.Show(tabPanel);
                    _tabGroupEvents[tabManager.Key]?.Invoke(tabPanel);
                    return;
                }
            }
        }

        public static void SubscribeToTabGroup(TabGroups group, Action<TabPanel> callback)
        {
            if (!_tabGroupEvents.ContainsKey(group))
            {
                _tabGroupEvents[group] = null;
            }

           _tabGroupEvents[group] += callback;
        }

        public static void UnsubscribeFromTabGroup(TabGroups group, Action<TabPanel> callback)
        {
            if (_tabGroupEvents.ContainsKey(group))
            {
                _tabGroupEvents[group] -= callback;
            }
        }
    }
}