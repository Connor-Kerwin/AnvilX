using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnvilX
{
    /// <summary>
    /// Orchestrates core functionality of the dependency framework.
    /// </summary>
    public static class RegistryCore
    {
        private static ObjectRegistry globalRegistry;
        private static readonly Dictionary<int, ObjectRegistry> index = new();

        public static ObjectRegistry GetGlobalRegistry()
        {
            if (globalRegistry)
            {
                return globalRegistry;
            }

            // NOTE: If we don't have a global registry, we are assuming that we're in an initialization state.
            // So we can happily kick off initialization. The only way this can be wrong, is if someone
            // destroys the global registry, which is not allowed!
            
            InitializeIndex();
            InitializeGlobalRegistry();
            return globalRegistry;
        }

        /// <summary>
        /// Attempt to find the <see cref="ObjectRegistry"/> for <paramref name="scene"/>.
        /// </summary>
        /// <param name="scene">The scene to query.</param>
        /// <returns>The registry in <paramref name="scene"/>. If <see cref="Scene.IsValid"/>
        /// is <see langword="false"/> for <paramref name="scene"/>, or if nothing in <paramref name="scene"/>
        /// uses <see cref="AnvilX"/>, <see langword="null"/> will be returned.</returns>
        public static ObjectRegistry? FindRegistry(Scene scene)
        {
            index.TryGetValue(scene.handle, out var result);    
            return result;
        }
        
        /// <summary>
        /// Get or create the <see cref="ObjectRegistry"/> for <paramref name="scene"/>.
        /// </summary>
        /// <param name="scene">The scene to query.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if <see cref="Scene.IsValid"/> is <see langword="false"/> for <paramref name="scene"/>.</exception>
        public static ObjectRegistry EnsureRegistry(Scene scene)
        {
            if (!scene.IsValid())
            {
                throw new ArgumentException("Cannot create a registry in an invalid scene.", nameof(scene));
            }
            
            // Registry exists for scene
            if (index.TryGetValue(scene.handle, out var registry))
            {
                return registry;
            }

            var parentRegistry = GetGlobalRegistry();
            var instance = new GameObject("[ObjectRegistry]");
            registry = instance.AddComponent<ObjectRegistry>();
            registry.InitRegistry(parentRegistry);

            index[scene.handle] = registry;

            SceneManager.MoveGameObjectToScene(instance, scene);

            return registry;
        }

        /// <summary>
        /// Get or create the <see cref="ObjectRegistry"/> for <paramref name="target"/>.
        /// The registry will be resolved based on the scene that <paramref name="target"/> resides in.
        /// </summary>
        /// <param name="target">The target</param>
        /// <returns>The relevant <see cref="ObjectRegistry"/> for <paramref name="target"/>.</returns>
        public static ObjectRegistry EnsureRegistry(GameObject target)
        {
            ParameterValidation.ThrowIfNull(target, nameof(target));
            
            var instance = target.GetComponentInParent<ObjectRegistry>();
            if (instance)
            {
                return instance;
            }

            return EnsureRegistry(target.gameObject.scene);
        }
        
        private static void InitializeIndex()
        {
            // NOTE: In the Unity editor, we want to support domain reloading, so we clear out the index
            // whenever we've hit a reset scenario.
            
            index.Clear();
        }

        private static void InitializeGlobalRegistry()
        {
            var instance = new GameObject("[ObjectRegistry]");
            UnityEngine.Object.DontDestroyOnLoad(instance);
            
            var registry = instance.AddComponent<ObjectRegistry>();
            registry.InitRegistry(null);
            
            index[instance.scene.handle] = registry;
            globalRegistry = registry;
        }
    }
}