using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// Automatically registers its own scene to be associated with a particular <see cref="ServiceScope"/>.
    /// </summary>
    public class RegisterServiceScope : MonoBehaviour
    {
        private ObjectRegistry registry;

        /// <summary>
        /// The scope to register the scene as.
        /// </summary>
        public ServiceScope Scope;

        private void Awake()
        {
            registry = RegistryCore.EnsureRegistry(gameObject);
            Scope.Register(registry);
        }

        private void OnDestroy()
        {
            if (registry is null)
            {
                return;
            }

            Scope.Unregister(registry);
        }
    }
}