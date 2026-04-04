using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// The base implementation of an object that consumes services.
    /// </summary>
    public abstract class Feature : MonoBehaviour
    {
        protected ObjectRegistry Registry { get; private set; }
        
        protected virtual void Start()
        {
            Registry = RegistryCore.FindRequiredRegistry(gameObject);
        }
    }
}