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
            var all = new List<GlobalAssetManifest>();
            GlobalAssetManifest.GetAllManifests(all);

            foreach (var manifest in all)
            {
                foreach (var asset in manifest.Prefabs)
                {
                    // This can happen sometimes...
                    if (asset == null)
                    {
                        continue;
                    }

                    Instantiate(asset);
                }
            }
        }

        /// <summary>
        /// Instantiate <paramref name="prefab"/> into the global scene.
        /// </summary>
        /// <param name="prefab">The prefab to spawn.</param>
        /// <typeparam name="T">The type of the prefab.</typeparam>
        /// <returns>The spawned prefab.</returns>
        public static T Instantiate<T>(T prefab)
            where T : Object
        {
            // NOTE: Due to execution order constraints, we must resolve the global registry,
            // then use that to provide a scene reference to bootstrap the instantiation.
            // It's not possible to instantiate the objects, then transfer them, as Awake
            // would have already fired.

            var globalScene = GlobalSceneTracker.GetGlobalScene();
            var parameters = new InstantiateParameters
            {
                scene = globalScene
            };

            var instance = Object.Instantiate(prefab, parameters);
            instance.name = prefab.name; // Clean up the name

            return instance;
        }
    }
}