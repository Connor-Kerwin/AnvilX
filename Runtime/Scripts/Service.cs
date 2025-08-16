using UnityEngine;

namespace AnvilX
{
    [DefaultExecutionOrder(-1000)]
    public abstract class Service : GameBehaviour
    {
        
    }

    public abstract class Service<T> : Service
        where T : GameBehaviour
    {
        protected virtual void Awake()
        {
            RegisterSelfAs<T>();
        }

        protected virtual void OnDestroy()
        {
            Unregister<T>();
        }
    }

    public abstract class Service<T1, T2> : Service
        where T1 : GameBehaviour
        where T2 : GameBehaviour
    {
        protected virtual void Awake()
        {
            RegisterSelfAs<T1>();
            RegisterSelfAs<T2>();
        }

        protected virtual void OnDestroy()
        {
            Unregister<T1>();
            Unregister<T2>();
        }
    }
}