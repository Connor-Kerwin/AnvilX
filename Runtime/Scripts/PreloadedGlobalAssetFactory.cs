using System.Collections.Generic;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// A factory that hooks into the <see cref="RegistryOrchestrator"/> init flow, facilitating the creation of
    /// global assets that are registered in the preloaded assets list.
    /// </summary>
    public static class PreloadedGlobalAssetFactory
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            InitGlobalAssets();
        }
        
        private static void InitGlobalAssets()
        {
            var globalRegistry = RegistryOrchestrator.GetGlobalRegistry();
            var globalScene = globalRegistry.gameObject.scene;
            
            var all = new HashSet<GlobalAssetManifest>();
            GlobalAssetManifest.GetLoadedManifests(all);

            foreach (var manifest in all)
            {
                foreach (var asset in manifest.Prefabs)
                {
                    _ = UnityEngine.Object.Instantiate(asset, globalScene);
                }
            }
        }
    }
}