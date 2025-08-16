using System;

namespace AnvilX.Sample
{
    public class ResolveARequiredDependency : Feature
    {
        public MyRequiredDependency result;
        
        private void Start()
        {
            RegisterTheDependency();
            
            // We can resolve a required dependency any time by using ResolveRequiredDependency.
            // If the dependency is not found, an exception will be thrown.
            
            result = ResolveRequiredDependency<MyRequiredDependency>();
            
            // This approach is recommended, because it helps to avoid situations where
            // null references are silently ignored, breaking later on, causing hard to find bugs!
        }

        private void RegisterTheDependency()
        {
            // Here, we imitate the registration flow that will happen elsewhere to register the thing

            var thing = new MyRequiredDependency()
            {
                someValue = "hello! I am a required dependency"
            };
            
            RegisterItemAs<MyRequiredDependency>(thing);
        }
        
        [Serializable]
        public class MyRequiredDependency
        {
            public string someValue;
        }
    }
}