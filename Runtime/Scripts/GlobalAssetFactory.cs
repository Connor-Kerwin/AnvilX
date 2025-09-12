using System.Collections.Generic;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// A factory that facilitating the creation of global assets.
    /// Automatically resolves and spawns the registered global assets.
    /// Also provides the means to manually spawn a global asset yourself.
    /// </summary>
    public static class GlobalAssetFactory
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            InitGlobalAssets();
        }

        private static void InitGlobalAssets()
        {
            var all = new HashSet<GlobalAssetManifest>();
            GlobalAssetManifest.GetLoadedManifests(all);

            foreach (var manifest in all)
            {
                foreach (var asset in manifest.Prefabs)
                {
                    // This is a valid 'unity' scenario.
                    if (asset == null)
                    {
                        continue;
                    }
                    
                    Instantiate(asset);
                }
            }
        }
        
        public static T Instantiate<T>(T prefab)
            where T : UnityEngine.Object
        {
            // NOTE: Due to execution order constraints, we must resolve the global registry,
            // then use that to provide a scene reference to bootstrap the instantiation.
            // It's not possible to instantiate the objects, then transfer them, as Awake
            // would have already fired.
            
            var globalRegistry = RegistryCore.GetGlobalRegistry();
            var globalScene = globalRegistry.gameObject.scene;
            var parameters = new InstantiateParameters
            {
                scene = globalScene
            };
            
            return UnityEngine.Object.Instantiate<T>(prefab, parameters);
        }
    }
}