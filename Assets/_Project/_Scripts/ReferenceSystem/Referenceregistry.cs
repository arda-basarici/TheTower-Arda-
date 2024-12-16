using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
    public class ReferenceRegistry
    {
        private Dictionary<ReferenceKeys, GameObject> references = new Dictionary<ReferenceKeys, GameObject>();

        public void Register(ReferenceKeys key, GameObject obj)
        {
            if (references.ContainsKey(key))
            {
                Debug.LogWarning($"Key '{key}' is already registered.");
                return;
            }
            references.Add(key, obj);
        }

        public void Unregister(ReferenceKeys key)
        {
            references.Remove(key);
        }

        public GameObject Get(ReferenceKeys key)
        {
            references.TryGetValue(key, out var obj);
            return obj;
        }

        public void ClearDestroyed()
        {
            references = references.Where(kv => kv.Value != null).ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}