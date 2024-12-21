using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public static class PoolManager
    {
        private static Dictionary<string, Pool> pools = new Dictionary<string, Pool>();
        private static PoolCleaner poolCleaner;
        private static Transform globalPoolParent;

        private static Pool LastUsedPool = null;
        private static readonly Queue<(string, Pool)> recentPools = new Queue<(string, Pool)>();

        private static readonly object poolLock = new object();
        private const int maxRecentPools = 5;
        private const float CleaningInterval = 10f;
        private const float InactivityThreshold = 60f;


        static PoolManager()
        {
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;
            InitializeCleaner();
        }

        private static void OnSceneUnloaded(Scene current)
        {
            ClearAllPools();
        }

        private static void InitializeCleaner()
        {
            if (poolCleaner != null) return;
            GameObject cleanerObject = new GameObject("PoolCleaner");
            Object.DontDestroyOnLoad(cleanerObject);
            poolCleaner = cleanerObject.AddComponent<PoolCleaner>();
            poolCleaner.Initialize(CleaningInterval);
        }
        private static void CreatePool(GameObject prefab, int initialSize = 0)
        {
            if (pools.ContainsKey(prefab.name))
            {
                Debug.LogWarning($"Pool with name {prefab.name} already exists.");
                return;
            }
            Transform parent = new GameObject(prefab.name + " Pool").transform;

            parent.parent = GetGlobalPoolParent().transform;
            Pool pool = new Pool(prefab, parent, initialSize);
            pools.Add(prefab.name, pool);
        }


        private static Transform GetGlobalPoolParent()
        {
            if (globalPoolParent == null)
            {
                var parentObj = GameObject.Find("Pools");
                if (parentObj == null)
                {
                    parentObj = new GameObject("Pools");
                }
                globalPoolParent = parentObj.transform;
            }
            return globalPoolParent;
        }

        public static void CleanUpPools()
        {
            foreach (var pool in pools.Values)
            {
                if (Time.time - pool.LastUsedTime > InactivityThreshold)
                {
                    pool.ShrinkPool();
                }
            }
        }

        public static void ClearAllPools()
        {
            foreach (var pool in pools.Values)
            {
                pool.ClearPool();
            }
            pools.Clear();
        }


        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            lock (poolLock)
            {
                string prefabName = prefab.name;
                GameObject obj = null;
                List<IPoolable> poolables;
                // Check the last-used pool
                if (LastUsedPool != null && pools.TryGetValue(prefabName, out var cachedPool) && cachedPool == LastUsedPool)
                {
                    obj = InstantiateFromPool(cachedPool, position, rotation, parent);
                    poolables = obj.GetComponents<IPoolable>().ToList();
                    foreach (var poolable in poolables)
                    {
                        poolable.ResetForPooling();
                        poolable.OnSpawn();
                    }
                    return obj;
                }

                // Check the recent pools cache
                foreach (var (name, pool) in recentPools)
                {
                    if (name == prefabName)
                    {
                        LastUsedPool = pool;
                        obj = InstantiateFromPool(pool, position, rotation, parent);
                        poolables = obj.GetComponents<IPoolable>().ToList();
                        foreach (var poolable in poolables)
                        {
                            poolable.ResetForPooling();
                            poolable.OnSpawn();
                        }
                        return obj;
                    }
                }

                if (!pools.ContainsKey(prefabName))
                {
                    CreatePool(prefab);
                }
                obj = pools[prefabName].GetObject(position, rotation, parent);
                poolables = obj.GetComponents<IPoolable>().ToList();
                foreach (var poolable in poolables)
                {
                    poolable.ResetForPooling();
                    poolable.OnSpawn();
                }
                Pool targetPool = pools[prefabName];
                CachePool(prefabName, targetPool);
                return obj;
            }
        }

        private static GameObject InstantiateFromPool(Pool pool, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject obj = pool.GetObject(position, rotation, parent);
            var poolables = obj.GetComponents<IPoolable>();
            foreach (var poolable in poolables)
            {
                poolable.ResetForPooling();
                poolable.OnSpawn();
            }
            return obj;
        }

        private static void CachePool(string prefabName, Pool pool)
        {
            LastUsedPool = pool;
            if (recentPools.Count >= maxRecentPools)
            {
                recentPools.Dequeue();
            }
            recentPools.Enqueue((prefabName, pool));
        }

        public static void ReturnToPool(GameObject obj)
        {
            string objectName = obj.name.Replace("(Clone)", "").Trim();
            if (!pools.ContainsKey(objectName))
            {
                Debug.LogWarning($"Pool with name {obj.name} does not exist.");
                Object.Destroy(obj);
                return;
            }
            pools[objectName].RemoveObject(obj);
            var poolables = obj.GetComponents<IPoolable>();
            foreach (var poolable in poolables)
            {
                poolable.OnReturn();
            }
        }


        // if you want to register a pool with a specific size, and warm it up
        public static void RegisterPool(GameObject prefab, int initialSize)
        {
            if (!pools.ContainsKey(prefab.name))
            {
                CreatePool(prefab, initialSize);
            }
        }

        public static void LogPoolStatus()
        {
            foreach (var pool in pools)
            {
                Debug.Log($"Pool: {pool.Key}, Active: {pool.Value.ActiveCount}, Inactive: {pool.Value.InactiveCount}");
            }
        }
    }
}