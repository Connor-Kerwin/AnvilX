using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnvilX
{
    [DefaultExecutionOrder(-10000)]
    public class ObjectRegistry : MonoBehaviour, IObjectResolver, IEnumerable<KeyValuePair<Type, object>>
    {
        private bool ready;
        private Dictionary<Type, object> index;
        
        // TODO: In scenarios where registries are duplicated, should we break when used? Probably!

        [Tooltip("The global priority of this registry, used to decide which registries to scan first when resolving a cross-scene dependency.")]
        [SerializeField] private int priority;
        
        [SerializeField]
        [Tooltip("The service groups to write to. This exposes registered objects in this scene to other scenes, allowing cross-scene object resolution.")]
        internal List<ServiceGroup> writeTo = new();
        
        [SerializeField] 
        [Tooltip("The service groups to read from when resolving a dependency that is not in the scene.")]
        internal List<ServiceGroup> readFrom = new();

        /// <summary>
        /// The global priority of this registry, used when systems are deciding between multiple <see cref="ObjectRegistry"/> to scan.
        /// </summary>
        public int Priority => priority;

        /// <summary>
        /// The number of items stored in the registry.
        /// </summary>
        public int Count => index.Count;

        private void Awake()
        {
            index = new Dictionary<Type, object>();
            
            // Handle failure scenario
            if (!RegistryCore.Add(this))
            {
                Debug.LogError(
                    $"An instance of {nameof(ObjectRegistry)} already exists within this scene. You can only have one per scene!", this);
                return;
            }

            ready = true;
        }

        private void OnDestroy()
        {
            if (ready)
            {
                RegistryCore.Remove(this);
            }
        }

        public void Register(object target, Type type)
        {
            ThrowIfContains(type);

            if (!type.IsAssignableFrom(target.GetType()))
            {
                throw new ArgumentException($"Target type {target.GetType()} is not assignable to {type}");
            }

            index.Add(type, target);
        }
        
        public void Unregister(Type type)
        {
            index.Remove(type);
        }
        
        public void Register<T>(T target)
        {
            Register(target, typeof(T));
        }

        public void Unregister<T>()
        {
            Unregister(typeof(T));
        }
        
        public object Resolve(Type type)
        {
            // First-pass, search self
            var result = ResolveObject(type);

            // Second-pass, search service groups
            if (result == null)
            {
                foreach (var scope in readFrom)
                {
                    result = scope.Resolve(type);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        internal object ResolveObject(Type type)
        {
            index.TryGetValue(type, out var result);
            return result;
        }

        private void ThrowIfContains(Type type)
        {
            if (index.ContainsKey(type))
            {
                throw new InvalidOperationException($"Duplicate item of type {type}");
            }
        }

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
        {
            return index.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}