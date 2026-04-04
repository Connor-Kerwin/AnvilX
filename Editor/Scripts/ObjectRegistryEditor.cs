using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AnvilX
{
    [CustomEditor(typeof(ObjectRegistry))]
    internal class ObjectRegistryEditor : Editor
    {
        private HashSet<ObjectRegistry> cache = new();

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            serializedObject.ApplyModifiedProperties();
            
            var registry = (ObjectRegistry)target;
            
            if (registry.didAwake)
            {
                EditorGUILayout.Space();
                DrawRegistryItemsRecursive(registry);
            }
        }

        private void DrawRegistryItemsRecursive(ObjectRegistry registry)
        {
            cache.Clear();

            cache.Add(registry);
            DrawRegistryItems(registry, "Registered Objects");
            EditorGUILayout.Space();

            foreach (var group in registry.readFrom)
            {
                foreach (var groupRegistry in group)
                {
                    
                    Debug.Log(groupRegistry.name);
                    if (!cache.Add(groupRegistry))
                    {
                        continue;
                    }

                    DrawRegistryItems(groupRegistry, group.name);
                    EditorGUILayout.Space();
                }
            }
        }

        private void DrawRegistryItems(ObjectRegistry registry, string header)
        {
            //var suffix = GetRegistrySuffix(registry);

            GUILayout.Label(header, EditorStyles.boldLabel);

            foreach (var kvp in registry)
            {
                if (kvp.Value is Object uObj)
                {
                    EditorGUILayout.ObjectField($"{kvp.Key.Name}", uObj, typeof(Object), true);
                }
                else
                {
                    EditorGUILayout.LabelField($"{kvp.Key.Name}", $"{kvp.Value}");
                }
            }
        }
    }
}