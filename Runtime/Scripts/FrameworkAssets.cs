using System;
using System.Linq;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// Exposes a set of assets that the framework uses.
    /// </summary>
    [CreateAssetMenu(menuName = "AnvilX/Debug/Create Framework Asset")]
    internal class FrameworkAssets : ScriptableObject
    {
        private static FrameworkAssets assets;

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
    }
}