using System;
using UnityEngine;

namespace Game
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class InstantiationUtility
    {

        public static Dictionary<GameObject, Action<GameObject>> callBackRegistry = new Dictionary<GameObject, Action<GameObject>>();
        public static GameObject InstantiateWithCallback(GameObject prefab, Transform parent, Action<GameObject> callback)
        {
            GameObject instance = UnityEngine.Object.Instantiate(prefab, parent);
            if(callback == null)
            {
                Debug.LogWarning("InstantiateWithCallback has no callback assigned", instance);
            }
            var callbackComponent = instance.AddComponent<Game.InstantiateCallback>();
            callbackComponent.OnReady = callback;
            callBackRegistry.Add(instance, callback);
            callbackComponent.Initialize();


            return instance;
        }
    }

}