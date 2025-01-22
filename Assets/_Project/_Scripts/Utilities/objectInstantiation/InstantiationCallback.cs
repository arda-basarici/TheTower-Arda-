using System;
using UnityEngine;

namespace Game
{
	public class InstantiateCallback : MonoBehaviour
	{
		public Action<GameObject> OnReady;

		public void Initialize()
		{
			Debug.Log("InstantiationCallback initialized");
            if (OnReady == null)
			{
				Debug.LogWarning("InstantiateCallback has no callback assigned", this);
            }
            OnReady?.Invoke(gameObject);
			InstantiationUtility.callBackRegistry.Remove(gameObject);

            Destroy(this);
		}

        public void Awake()
        {
            Debug.Log("Awake");
        }
    }
}
