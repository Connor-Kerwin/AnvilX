#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnvilX
{
    public static class RegistryCore
    {
        private static readonly Dictionary<SceneHandle, ObjectRegistry> Index = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void InitializeGlobalObjectRegistry()
        {
            // We clear here because we're initializing the whole system
            Index.Clear();
            
            // Spawn in the global registry
            var frameworkAssets = FrameworkAssets.GetFrameworkAssets();
            GlobalAssetFactory.Instantiate(frameworkAssets.GlobalObjectRegistryPrefab);
        }
        
        /// <summary>
        /// Add <paramref name="registry"/>.
        /// </summary>
        /// <param name="registry">The registry to add.</param>
        /// <returns>Whether the registry was added. Failure is typically caused by having multiple instances in a single scene.</returns>
        internal static bool Add(ObjectRegistry registry)
        {
            var handle = registry.gameObject.scene.handle;
            if (!Index.TryAdd(handle, registry))
            {
                return false;
            }
            
            // Associate the registry with chosen write scopes
            foreach (var group in registry.writeTo)
            {
                if (!group)
                {
                    continue;
                }
               
                group.Add(registry);
            }
            
            return true;
        }
        
        /// <summary>
        /// Remove <paramref name="registry"/>.
        /// </summary>
        /// <param name="registry">The item to unregister.</param>
        internal static void Remove(ObjectRegistry registry)
        {
            var scene = registry.gameObject.scene;
            Index.Remove(scene.handle);

            // Detach the registry from associated groups
            foreach (var group in registry.writeTo)
            {
                if (!group)
                {
                    continue;
                }

                group.Remove(registry);
            }
        }

        public static ObjectRegistry? FindRegistry(GameObject target)
        {
            return FindRegistry(target.scene);
        }

        public static ObjectRegistry? FindRegistry(Scene scene)
        {
            Index.TryGetValue(scene.handle, out var registry);
            return registry;
        }

        public static ObjectRegistry FindRequiredRegistry(GameObject target)
        {
            var registry = FindRegistry(target);
            return !registry ? throw new Exception("Required registry not found") : registry;
        }

        public static ObjectRegistry FindRequiredRegistry(Scene scene)
        {
            var registry = FindRegistry(scene);
            return !registry ? throw new Exception("Required registry not found") : registry;
        }
    }
}