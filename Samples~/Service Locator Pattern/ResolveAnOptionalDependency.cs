using System;

namespace AnvilX.Sample
{
    public class ResolveAnOptionalDependency : Feature
    {
        public MyOptionalDependency result;
        
        private void Start()
        {
            RegisterTheDependency();
            
            // We can resolve a service any time by using ResolveDependency.
            // If the dependency is not found, null is returned.

            result = ResolveDependency<MyOptionalDependency>();
        }

        private void RegisterTheDependency()
        {
            // Here, we imitate the registration flow that will happen elsewhere to register the thing

            var thing = new MyOptionalDependency()
            {
                someValue = "hello! I am an optional dependency"
            };
            
            RegisterItemAs<MyOptionalDependency>(thing);
        }

        [Serializable]
        public class MyOptionalDependency
        {
            public string someValue;
        }
    }
}