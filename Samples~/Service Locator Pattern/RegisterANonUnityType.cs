using System;

namespace AnvilX.Sample
{
    public class RegisterANonUnityType : Service
    {
        public MySampleNonUnityType result;
        
        private void Awake()
        {
            // You can register non-unity objects too!

            result = new MySampleNonUnityType
            {
                SomeData = "hello! I am a non unity type that has been registered as a dependency"
            };
            
            RegisterItemAs<MySampleNonUnityType>(result);

            // OR
            
            // var registry = GetObjectRegistry();
            // registry.Register<MyCustomNonUnityType>(myClass);
        }
    }
    
    [Serializable]
    public class MySampleNonUnityType
    {
        public string SomeData;
    }
}