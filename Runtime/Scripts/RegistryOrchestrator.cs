using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnvilX
{
    /// <summary>
    /// Orchestrates core functionality of the dependency framework.
    /// </summary>
    public static class RegistryOrchestrator
    {
        private static ObjectRegistry globalRegistry;
        private static readonly Dictionary<int, ObjectRegistry> index = new();

        public static ObjectRegistry GetGlobalRegistry()
        {
            if (globalRegistry)
            {
                return globalRegistry;
            }

            InitGlobalRegistry();
            return globalRegistry;
        }
        
        /// <summary>
        /// Get or create the dependency registry for <paramref name="scene"/>.
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static ObjectRegistry GetSceneRegistry(Scene scene)
        {
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
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            index.Clear();
        }

        private static void InitGlobalRegistry()
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