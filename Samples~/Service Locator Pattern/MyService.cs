using System;

namespace AnvilX
{
    public class MyService : Service<MyService>
    {
        // NOTE: No boilerplate code is required for service registration.

        protected override void Awake()
        {
            // NOTE: base.Awake() is where service registration actually takes place.
            // Make sure to call it if you are overriding Awake!
            
            base.Awake();
        }
    }
}