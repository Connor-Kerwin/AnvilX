using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// Represents a conceptual scope to categorize particular <see cref="ObjectRegistry"/> instances
    /// into data-driven concepts. This offers a decoupled way to access groups of services without
    /// directly looking at particular scenes.
    /// </summary>
    [CreateAssetMenu(fileName = "ServiceScope", menuName = "AnvilX/Service Scope")]
    public class ServiceScope : ScriptableObject, IEnumerable<ObjectRegistry>
    {
        private readonly HashSet<ObjectRegistry> items = new();

        /// <summary>
        /// The number of <see cref="ObjectRegistry"/> associated with the scope.
        /// </summary>
        public int Count => items.Count;
        
        /// <summary>
        /// Register <paramref name="item"/> to this scope.
        /// </summary>
        /// <param name="item"></param>
        public void Register(ObjectRegistry item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Remove <paramref name="item"/> from this scope.
        /// </summary>
        /// <param name="item"></param>
        public void Unregister(ObjectRegistry item)
        {
            items.Remove(item);
        }
        
        /// <summary>
        /// Find the first occurence of an item by <paramref name="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            foreach (var registry in items)
            {
                var item = registry.Resolve(type);
                if (item != null)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Find the first occurrence of an item by type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        // TODO: Expose a ResolveAll variant to find all instances
        
        public IEnumerator<ObjectRegistry> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
