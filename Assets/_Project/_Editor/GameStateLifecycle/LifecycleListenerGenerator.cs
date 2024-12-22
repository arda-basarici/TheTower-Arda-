using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Game;
using UnityEditor;
using UnityEngine;

public class LifecycleCodeGenerator : Editor
{
    [MenuItem("Tools/Generate Lifecycle Listeners")]
    public static void GenerateLifecycleListeners()
    {
        string outputPath = "Assets/_Project/_Scripts/GameManager/Generated/LifecycleListeners.cs";

        // Ensure output directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

        // Get all types implementing the IGameStateListener
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var listenerTypes = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => typeof(IGameStateListener).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        // Generate the lifecycle registration code
        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            writer.WriteLine("// Auto-generated lifecycle listener registration");
            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine("namespace Game");
            writer.WriteLine("{");
            writer.WriteLine("    public static class LifecycleManagerGenerated");
            writer.WriteLine("    {");
            writer.WriteLine("        public static void RegisterAllListeners()");
            writer.WriteLine("        {");

            foreach (var type in listenerTypes)
            {
                writer.WriteLine($"            LifecycleManager.Register(new {type.FullName}());");
            }

            writer.WriteLine("        }");
            writer.WriteLine("    }");
            writer.WriteLine("}");
        }

        // Refresh the Asset Database
        AssetDatabase.Refresh();

        Debug.Log("Lifecycle listeners generated successfully!");
    }
}
