using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AnvilX
{
    [CustomEditor(typeof(GlobalAssetManifest))]
    internal class GlobalAssetManifestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var isAssigned = false;
            var preloadedAssets = PlayerSettings.GetPreloadedAssets();
            foreach (var asset in preloadedAssets)
            {
                if (asset == target)
                {
                    isAssigned = true;
                    break;
                }
            }

            // NOTE: For easy setup, we want to provide a convenience button for automatically adding the manifest to the preloaded asset list.
            
            if (!isAssigned)
            {
                EditorGUILayout.HelpBox(
                    "This asset manifest has not been assigned to the preloaded asset list. These global assets may not be loaded!",
                    MessageType.Warning);
                if (GUILayout.Button("Add to preloaded assets"))
                {
                    var allAssets = PlayerSettings.GetPreloadedAssets().ToList();
                    allAssets.Add(target);
                    PlayerSettings.SetPreloadedAssets(allAssets.ToArray());
                }
            }
        }
    }
}