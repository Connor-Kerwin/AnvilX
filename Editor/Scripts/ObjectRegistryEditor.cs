using UnityEditor;
using UnityEngine;

namespace AnvilX
{
    [CustomEditor(typeof(ObjectRegistry))]
    internal class ObjectRegistryEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            serializedObject.ApplyModifiedProperties();

            var registry = (ObjectRegistry)target;
            if (registry.didAwake)
            {
                EditorGUILayout.LabelField("Registered Objects", EditorStyles.boldLabel);
                DrawRegistryItemsRecursive(registry);
            }
        }

        private void DrawRegistryItemsRecursive(ObjectRegistry registry)
        {
            while (registry)
            {
                DrawRegistryItems(registry);
                registry = registry.ParentRegistry;
            }
        }

        private string GetRegistrySuffix(ObjectRegistry registry)
        {
            var isInherited = registry != (ObjectRegistry)target;
            if (isInherited)
            {
                return "(inherited)";
            }

            return "";
        }

        private void DrawRegistryItems(ObjectRegistry registry)
        {
            var suffix = GetRegistrySuffix(registry);

            foreach (var kvp in registry)
            {
                if (kvp.Value is Object uObj)
                {
                    EditorGUILayout.ObjectField($"{kvp.Key.Name} {suffix}", uObj, typeof(Object), true);
                }
                else
                {
                    EditorGUILayout.LabelField($"{kvp.Key.Name} {suffix}", $"{kvp.Value}");
                }
            }
        }
    }
}