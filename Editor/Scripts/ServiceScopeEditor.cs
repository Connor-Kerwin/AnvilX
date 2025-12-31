using UnityEditor;

namespace AnvilX
{
    [CustomEditor(typeof(ServiceScope))]
    public class ServiceScopeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            
            EditorGUILayout.HelpBox(
                $"At runtime, you will see a list of all current registered {nameof(ObjectRegistry)} associated with this scope below.",
                MessageType.Info);
            
            EditorGUILayout.Space();

            var identifier = (ServiceScope)target;
            foreach (var registry in identifier)
            {
                // Item can be NULL in scenarios where the registry has been deleted (shouldn't happen!)
                if (!registry)
                {
                    continue;
                }

                var label = registry.gameObject.scene.name;
                EditorGUILayout.ObjectField(label, registry, typeof(ServiceScope), true);
            }
        }
    }
}