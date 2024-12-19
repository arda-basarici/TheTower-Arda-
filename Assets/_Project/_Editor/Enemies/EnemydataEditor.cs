//using UnityEngine;
//using UnityEditor;
//using Game;

//[CustomEditor(typeof(EnemyData))]
//public class EnemyDataEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        // Draw default inspector fields
//        DrawDefaultInspector();

//        // Reference to the EnemyData target
//        EnemyData enemyData = (EnemyData)target;

//        // Add a button to generate Prefab and Stats
//        if (GUILayout.Button("Generate Prefab and Stats"))
//        {
//            GeneratePrefabAndStats(enemyData);
//        }
//    }

//    private void GeneratePrefabAndStats(EnemyData enemyData)
//    {
//        string enemyName = enemyData.enemyName;

//        // Define paths for the new assets
//        string sourcePrefabFolder = "Assets/Resources/Enemies/Prefabs";
//        string prefabPath = $"{sourcePrefabFolder}/{enemyName}.prefab";
//        string statsPath = $"Assets/Resources/Enemies/Stats/{enemyName}.asset";

//        // Ensure Resources subfolders exist
//        System.IO.Directory.CreateDirectory("Assets/Resources/Enemies/Prefabs");
//        System.IO.Directory.CreateDirectory("Assets/Resources/Enemies/Stats");

//        // Find an existing prefab in the folder
//        string[] prefabGUIDs = AssetDatabase.FindAssets("t:GameObject", new[] { sourcePrefabFolder });
//        if (prefabGUIDs.Length > 0)
//        {
//            string sourcePrefabPath = AssetDatabase.GUIDToAssetPath(prefabGUIDs[0]); // Get the first prefab found
//            GameObject sourcePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePrefabPath);

//            if (sourcePrefab != null)
//            {
//                // Duplicate the prefab
//                PrefabUtility.SaveAsPrefabAsset(sourcePrefab, prefabPath);
//                Debug.Log($"Copied prefab from {sourcePrefabPath} to {prefabPath}");
//            }
//        }
//        else
//        {
//            Debug.LogWarning("No prefab found in the folder to copy.");
//            return;
//        }

//        // Create Stats ScriptableObject
//        StatData newStats = ScriptableObject.CreateInstance<StatData>();
//        AssetDatabase.CreateAsset(newStats, statsPath);

//        // Assign the created assets to the EnemyData fields
//        enemyData.stats = newStats;
//        enemyData.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

//        // Save and refresh the Asset Database
//        EditorUtility.SetDirty(enemyData);
//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();

//        Debug.Log($"Generated Prefab and Stats for {enemyName}.");
//    }

//}
