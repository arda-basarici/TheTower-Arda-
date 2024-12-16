using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Referencable : MonoBehaviour
    {
        [ValueDropdown("GetPredefinedKeys")]
        [SerializeField]
        private ReferenceKeys referenceKey;

        public ReferenceKeys ReferenceKey => referenceKey;

        protected void Awake()
        {
            if (referenceKey == ReferenceKeys.None)
            {
                Debug.LogWarning($"{gameObject.name} has an unassigned Reference Key.");
                return;
            }
            ReferenceResolver.Register(referenceKey, gameObject);
            Debug.Log($"Registered '{referenceKey}' in ReferenceResolver.");
        }

        protected void OnDestroy()
        {
            ReferenceResolver.Unregister(referenceKey);
            Debug.Log($"Unregistered '{referenceKey}' from ReferenceResolver.");
        }

        private static IEnumerable<ValueDropdownItem> GetPredefinedKeys()
        {
            return Enum.GetNames(typeof(ReferenceKeys))
                .Select(name => new ValueDropdownItem(name, Enum.Parse(typeof(ReferenceKeys), name)));
        }
    }
}
