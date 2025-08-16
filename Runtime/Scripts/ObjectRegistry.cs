using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnvilX
{
    [AddComponentMenu("")]
    public class ObjectRegistry : MonoBehaviour, IEnumerable<KeyValuePair<Type, object>>
    {
        private readonly Dictionary<Type, object> index = new();

        private ObjectRegistry parentRegistry;

        /// <summary>
        /// The total number of items registered into the registry.
        /// </summary>
        public int Count => index.Count;

        /// <summary>
        /// The parent registry that will be used as a fallback when resolving a dependency that doesn't exist in this registry.
        /// </summary>
        public ObjectRegistry ParentRegistry => parentRegistry;
        
        internal void InitRegistry(ObjectRegistry parent)
        {
            parentRegistry = parent;
        }

        private void ThrowIfContains(Type type)
        {
            if (index.ContainsKey(type))
            {
                throw new InvalidOperationException($"Duplicate item of type {type} in {name}");
            }
        }
        
        /// <summary>
        /// Register <paramref name="target"/> as <paramref name="type"/>.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <exception cref="InvalidOperationException">Thrown if the type is already registered.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="target"/> is not assignable to <paramref name="type"/>.</exception>
        public void Register(object target, Type type)
        {
            ThrowIfContains(type);

            if (!type.IsAssignableFrom(target.GetType()))
            {
                throw new ArgumentException($"Target type {target.GetType()} is not assignable to {type}");
            }

            index.Add(type, target);
        }

        /// <summary>
        /// Register <paramref name="target"/> as <typeparamref name="T"/>.
        /// </summary>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="InvalidOperationException">Thrown if the type is already registered.</exception>
        public void Register<T>(T target)
        {
            var type = typeof(T);
            ThrowIfContains(type);
            index.Add(type, target);
        }

        public void Unregister(Type type)
        {
            index.Remove(type);
        }

        public void Unregister<T>()
        {
            Unregister(typeof(T));
        }

        public object Resolve(Type type)
        {
            index.TryGetValue(type, out var result);

            if (result == null && parentRegistry)
            {
                result = parentRegistry.Resolve(type);
            }

            return result;
        }

        public T Resolve<T>()
            where T : class
        {
            return Resolve(typeof(T)) as T;
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