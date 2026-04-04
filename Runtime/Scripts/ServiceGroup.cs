#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// Provides the means to abstract services across the boundary of scenes.
    /// Registries from multiple scenes can self-subscribe to a group, exposing their services.
    /// </summary>
    [CreateAssetMenu(menuName = "AnvilX/Service Group")]
    public class ServiceGroup : ScriptableObject, IObjectResolver, IEnumerable<ObjectRegistry>
    {
        private readonly List<ObjectRegistry> index = new();

        public int Count => index.Count;
        
        public void Add(ObjectRegistry registry)
        {
            index.Add(registry);
            index.Sort((lhs, rhs) => lhs.Priority.CompareTo(rhs.Priority));
        }

        public void Remove(ObjectRegistry registry)
        {
            index.Remove(registry);
        }
        
        /// <summary>
        /// Resolve an object from the service group.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object? Resolve(Type type)
        {
            foreach (var candidate in index)
            {
                var instance = candidate.ResolveObject(type);
                if (instance != null)
                {
                    return instance;
                }
            }

            return null;
        }

        public IEnumerator<ObjectRegistry> GetEnumerator()
        {
            return index.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}