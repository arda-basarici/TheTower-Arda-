using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Pool
    {
        public string PrefabName => prefab.name;
        public int ActiveCount => TotalObjects - objects.Count;
        public int InactiveCount => objects.Count;
        private int TotalObjects => objects.Count + objects.Count(obj => obj.activeSelf);

        private Queue<GameObject> objects;
        private GameObject prefab;
        private Transform parent;

        public float LastUsedTime { get; private set; }
        private const int MaxPoolSize = 20; // Maximum number of objects in the pool, adjust as needed. dependency injection can be use  to set this value dynamically.
        
        public Pool(GameObject prefab, Transform parent, int initialSize = 0)
        {
            this.prefab = prefab;
            this.parent = parent;
            objects = new Queue<GameObject>();

            for (int i = 0; i < initialSize; i++)
            {
                AddObjectToPool();
            }

            UpdateLastUsedTime();
        }

        private void AddObjectToPool()
        {
            if (objects.Count >= MaxPoolSize)
            {
                Debug.LogWarning($"Pool for {prefab.name} reached maximum size.");
                return;
            }
            GameObject obj = Object.Instantiate(prefab, parent);
            obj.name = prefab.name;
            obj.SetActive(false);
            objects.Enqueue(obj);
        }

        public GameObject GetObject(Vector3 position, Quaternion rotation, Transform SpawnParent = null)
        {
            NullCheck();
            GameObject obj = objects.Dequeue();
            if(SpawnParent != null)
            {
                obj.transform.SetParent(SpawnParent);
            }
            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);
            return obj;
        }

        public void ShrinkPool()
        {
            while (objects.Count > 0)
            {
                GameObject obj = objects.Dequeue();
                Object.Destroy(obj);
            }

        }

        public void ClearPool()
        {
            foreach (var obj in objects)
            {
                Object.Destroy(obj);
            }
            objects.Clear();
        }

        private void UpdateLastUsedTime()
        {
            LastUsedTime = Time.time;
        }

        public void NullCheck()
        {
            while (objects.Count > 0 && objects.Peek() == null)
            {
                objects.Dequeue();
            }
            if (objects.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    AddObjectToPool();
                }

            }
        }

        public void RemoveObject(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(parent);
            objects.Enqueue(obj);
        }
    }
}