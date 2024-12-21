#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Game;


    [CustomEditor(typeof(Referencable))]
    public class ReferencableEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();

            Referencable referencable = (Referencable)target;

            if (HasDuplicateKey(referencable))
            {
                EditorGUILayout.HelpBox($"The key '{referencable.ReferenceKey}' is already used by another object in the scene!", MessageType.Error);
            }
        }

        private bool HasDuplicateKey(Referencable referencable)
        {

            Referencable[] allReferencables = FindObjectsByType<Referencable>( FindObjectsSortMode.None );

            foreach (var other in allReferencables)
            {
                if (other == referencable) continue;

                if (other.ReferenceKey == referencable.ReferenceKey)
                {
                    return true;
                }
            }

            return false;
        }
    }
#endif
