using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// A manifest of assets that will be automatically instantiated in a global scene during startup.
    /// </summary>
    [CreateAssetMenu(fileName = "New Global Asset Manifest", menuName = "AnvilX/Global Assets/Manifest")]
    public class GlobalAssetManifest : ScriptableObject
    {
        /// <summary>
        /// The set of prefabs that will be automatically instantiated.
        /// </summary>
        public GameObject[] Prefabs;
        
        internal static void GetAllManifests(List<GlobalAssetManifest> output)
        {
            #if UNITY_EDITOR
            var preloaded = UnityEditor.PlayerSettings.GetPreloadedAssets();
            foreach (var asset in preloaded)
            {
                if (asset is GlobalAssetManifest manifest)
                {
                    output.Add(manifest);
                }
            }
            #endif
        }

        /// <summary>
        /// A convenience function to make switching global asset manifests easier.
        /// </summary>
        [ContextMenu("Remove from preloaded assets", false)]
        private void RemoveFromPreloadedAssets()
        {
            #if UNITY_EDITOR
            var assets = UnityEditor.PlayerSettings.GetPreloadedAssets().ToList();
            if (assets.RemoveAll(t => t == this) > 0)
            {
                UnityEditor.PlayerSettings.SetPreloadedAssets(assets.ToArray());
            }
            
            #endif
        }
    }
}