using UnityEditor;
using UnityEngine;
using System.IO;

namespace Game
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int moneyReward;
        public int tokenReward;
        public StatData stats;
        public GameObject prefab;


#if UNITY_EDITOR
        private void OnEnable()
        {
           string assetPath = AssetDatabase.GetAssetPath(this);
           enemyName = Path.GetFileNameWithoutExtension(assetPath);

            EditorApplication.delayCall += () =>
            {
                if (stats == null || prefab == null)
                {
                    GenerateAssets();
                }
            };

        }

        private void GenerateAssets()
        {
            string prefabFolder = "Assets/Resources/Enemies/Prefabs";
            string statsFolder = "Assets/Resources/Enemies/Stats";

            string prefabPath = $"{prefabFolder}/{enemyName}.prefab";
            string statsPath = $"{statsFolder}/{enemyName}.asset";

            Directory.CreateDirectory(prefabFolder);
            Directory.CreateDirectory(statsFolder);

            string[] prefabGUIDs = AssetDatabase.FindAssets("t:GameObject", new[] { prefabFolder });
            if (prefabGUIDs.Length > 0)
            {
                string sourcePrefabPath = AssetDatabase.GUIDToAssetPath(prefabGUIDs[0]);
                GameObject sourcePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePrefabPath);

                if (sourcePrefab != null)
                {
                    PrefabUtility.SaveAsPrefabAsset(sourcePrefab, prefabPath);
                    prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                    Debug.Log($"Copied prefab to {prefabPath}");
                }
            }
            else
            {
                Debug.LogWarning("No prefab found to copy.");
            }

            string[] statGUIDs = AssetDatabase.FindAssets("t:StatData", new[] { statsFolder });
            if (statGUIDs.Length > 0)
            {
                string sourceStatPath = AssetDatabase.GUIDToAssetPath(statGUIDs[0]);
                StatData sourceStat = AssetDatabase.LoadAssetAtPath<StatData>(sourceStatPath);

                if (sourceStat != null)
                {
                    StatData newStats = Instantiate(sourceStat);
                    AssetDatabase.CreateAsset(newStats, statsPath);
                    stats = newStats;
                    Debug.Log($"Copied StatData to {statsPath}");
                }
            }
            else
            {
                Debug.LogWarning("No StatData found to copy.");
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Generated assets for {enemyName}.");
        }
#endif

    }
}