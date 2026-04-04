using System;
using UnityEditor;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// Exposes a set of assets that the framework uses.
    /// </summary>
    [CreateAssetMenu(menuName = "AnvilX/Debug/Create Framework Asset")]
    internal class FrameworkAssets : ScriptableObject
    {
        /// <summary>
        /// The asset which represents the global service group.
        /// </summary>
        [Tooltip("The asset which represents the global service group.")]
        public ServiceGroup GlobalServiceGroup;
        
        /// <summary>
        /// The prefab that is spawned as the global object registry.
        /// </summary>
        [Tooltip("The prefab that is spawned as the global object registry.")]
        public ObjectRegistry GlobalObjectRegistryPrefab;

        /// <summary>
        /// Get a reference to the framework assets.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static FrameworkAssets GetFrameworkAssets()
        {
            var preloaded = PlayerSettings.GetPreloadedAssets();
            foreach (var asset in preloaded)
            {
                if (asset is FrameworkAssets frameworkAssets)
                {
                    return frameworkAssets;
                }
            }

            throw new InvalidOperationException(
                "Failed to get framework assets. Configure your project by running AnvilX/Configure Framework Assets on the top bar");
        }
    }
}