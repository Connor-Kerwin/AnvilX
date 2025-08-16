using UnityEngine;

namespace AnvilX
{
    public class RegisterTypeManually : MonoBehaviour
    {
        private void Awake()
        {
            // You don't have to use the Service classes, they are just a good starting point.
            // Below is an example of what Service does under the hood.
            
            // RegistryOrchestrator provides us with the registry associated with the current scene
            var registry = RegistryOrchestrator.GetSceneRegistry(gameObject.scene);
            
            // We manually register this object by type.
            registry.Register<RegisterTypeManually>(this);
        }
    }
}