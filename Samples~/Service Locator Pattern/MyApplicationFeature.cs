using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnvilX.ServiceLocatorSample
{
    public class MyApplicationFeature : Feature
    {
        public MyService myService;
        
        // NOTE: Following the recommended best practices for Unity development,
        // Start is the best place to initiate service discovery. Awake is reserved
        // for self-initialization only, we should avoid reaching out to other objects
        // during awake!
        // 
        // As an exception, services can reach out to register themselves during awake.
        // This is because the framework initializes the scene-level ObjectRegistry
        // on demand, so it's not prone to order-related issues.
        
        private void Start()
        {
            // Resolve the service!
            myService = ResolveDependency<MyService>();
        }
    }
}

