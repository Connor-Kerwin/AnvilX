using System.Collections.Generic;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// A manifest of assets that will be automatically instantiated in a global scene during startup.
    /// </summary>
    [CreateAssetMenu(fileName = "New Asset Manifest", menuName = "Global Asset Manifest")]
    public class GlobalAssetManifest : ScriptableObject
    {
        /// <summary>
        /// The set of prefabs that will be automatically instantiated.
        /// </summary>
        public GameObject[] Prefabs;

        private void OnEnable()
        {
            // NOTE: For stability, we only perform manual registration in a built app.
            // We rely on Unity invoking OnEnable for our preloaded asset during start up.

#if !UNITY_EDITOR
        allManifests.Add(this);
#endif
        }

        private static readonly HashSet<GlobalAssetManifest> allManifests = new();

        /// <summary>
        /// Populate <paramref name="output"/> with all the loaded global asset manifests.
        /// </summary>
        /// <param name="output"></param>
        public static void GetLoadedManifests(ICollection<GlobalAssetManifest> output)
        {
#if UNITY_EDITOR
            BootstrapEditorManifests();
#endif
            
            foreach (var manifest in allManifests)
            {
                if (!manifest)
                {
                    continue;
                }

                output.Add(manifest);
            }
        }

        private static void BootstrapEditorManifests()
        {
#if UNITY_EDITOR
            // In editor, we have less control over the preloaded asset lifecycle.
            // What we can do, is manually query the preloaded assets to have a more stable
            // editor behaviour.
            
            // NOTE: This is a little wasteful, but to reduce reliance on execution order, we always
            // pull the preloaded assets fresh.
            
            allManifests.Clear();

            foreach (var asset in UnityEditor.PlayerSettings.GetPreloadedAssets())
            {
                var manifest = asset as GlobalAssetManifest;
                if (!manifest)
                {
                    continue;
                }

                allManifests.Add(manifest);
            }
#endif
        }
    }
}