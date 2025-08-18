using System.Collections.Generic;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// A factory that facilitating the creation of global assets
    /// that are registered in the preloaded assets list.
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
            // NOTE: Due to execution order constraints, we must resolve the global registry,
            // then use that to provide a scene reference to bootstrap the instantiation.
            // It's not possible to instantiate the objects, then transfer them, as Awake
            // would have already fired.
            
            var globalRegistry = RegistryCore.GetGlobalRegistry();
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