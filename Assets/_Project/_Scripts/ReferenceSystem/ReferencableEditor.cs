#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Game
{
    [CustomEditor(typeof(Referencable))]
    public class ReferencableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();

            // Get the target object
            Referencable referencable = (Referencable)target;

            // Check for duplicate keys in the scene
            if (HasDuplicateKey(referencable))
            {
                EditorGUILayout.HelpBox($"The key '{referencable.ReferenceKey}' is already used by another object in the scene!", MessageType.Error);
            }
        }

        private bool HasDuplicateKey(Referencable referencable)
        {
            // Get all Referencable objects in the scene
            Referencable[] allReferencables = FindObjectsByType<Referencable>( FindObjectsSortMode.None );

            foreach (var other in allReferencables)
            {
                // Skip the current object
                if (other == referencable) continue;

                // Check for duplicate keys
                if (other.ReferenceKey == referencable.ReferenceKey)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
#endif
