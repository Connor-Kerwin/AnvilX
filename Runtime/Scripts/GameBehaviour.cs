using System;
using UnityEngine;

namespace AnvilX
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        [NonSerialized]
        private ObjectRegistry objectRegistry;

        public ObjectRegistry GetObjectRegistry()
        {
            if (objectRegistry)
            {
                return objectRegistry;
            }

            objectRegistry = DiscoverRegistry();
            return objectRegistry;
        }

        /// <summary>
        /// Register <see langword="this"/> in the dependency graph as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RegisterSelfAs<T>()
            where T : GameBehaviour
        {
            var currentRegistry = GetObjectRegistry();
            currentRegistry.Register((T)this);
        }

        /// <summary>
        /// Register <paramref name="item"/> in the dependency graph as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void RegisterItemAs<T>(T item)
        {
            var currentRegistry = GetObjectRegistry();
            currentRegistry.Register(item);
        }

        public void Unregister<T>()
        {
            // Edge-case: If the registry doesn't exist, do nothing
            if (!objectRegistry)
            {
                return;
            }
            
            //var dependencyRegistry = GetDependencyRegistry();
            objectRegistry.Unregister<T>();
        }

        /// <summary>
        /// Attempt to resolve dependency <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ResolveDependency<T>()
            where T : class
        {
            var dependencyRegistry = GetObjectRegistry();
            return dependencyRegistry.Resolve<T>();
        }

        /// <summary>
        /// Attempt to resolve dependency <typeparamref name="T"/>.
        /// If the dependency doesn't exist, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T ResolveRequiredDependency<T>()
            where T : class
        {
            var dependencyRegistry = GetObjectRegistry();
            var item = dependencyRegistry.Resolve<T>();
            if (item == null)
            {
                throw new Exception($"Failed to resolve {typeof(T).Name}");
            }

            return item;
        }

        public Lazy<T> CreateLazyDependency<T>()
            where T : class
        {
            return new Lazy<T>(ResolveDependency<T>);
        }

        public Lazy<T> CreateLazyRequiredDependency<T>()
            where T : class
        {
            return new Lazy<T>(ResolveRequiredDependency<T>);
        }

        private ObjectRegistry DiscoverRegistry()
        {
            var instance = GetComponentInParent<ObjectRegistry>();
            if (instance)
            {
                return instance;
            }

            return RegistryOrchestrator.GetSceneRegistry(gameObject.scene);
        }
    }
}