using UnityEngine;

namespace AnvilX
{
    /// <summary>
    /// A base implementation for generic boilerplate service registration and cleanup.
    /// NOTE: Directly inheriting this class will not perform any self-registration or cleanup.
    /// </summary>
    public abstract class Service : MonoBehaviour
    {
        /// <summary>
        /// The object registry that the service is using.
        /// </summary>
        protected ObjectRegistry ObjectRegistry { get; private set; }
        
        protected virtual void Awake()
        {
            ObjectRegistry = RegistryCore.EnsureRegistry(gameObject);
        }
    }

    /// <summary>
    /// A single arg generic boilerplate class that provides automatic self registration
    /// and cleanup.
    /// </summary>
    /// <typeparam name="T">The type to register as.</typeparam>
    public abstract class Service<T> : Service
    {
        protected override void Awake()
        {
            base.Awake();
            
            ObjectRegistry.Register<T>((T)(object)this);
        }

        protected virtual void OnDestroy()
        {
            ObjectRegistry.Unregister<T>();
        }
    }

    /// <summary>
    /// A two arg generic boilerplate class that provides automatic self registration
    /// and cleanup.
    /// </summary>
    /// <typeparam name="T1">The first type to register as.</typeparam>
    /// <typeparam name="T2">The second type to register as.</typeparam>
    public abstract class Service<T1, T2> : Service
    {
        protected override void Awake()
        {
            base.Awake();
            
            ObjectRegistry.Register<T1>((T1)(object)this);
            ObjectRegistry.Register<T2>((T2)(object)this);
        }

        protected virtual void OnDestroy()
        {
            ObjectRegistry.Unregister<T1>();
            ObjectRegistry.Unregister<T2>();
        }
    }
    
    /// <summary>
    /// A three arg generic boilerplate class that provides automatic self registration
    /// and cleanup.
    /// </summary>
    /// <typeparam name="T1">The first type to register as.</typeparam>
    /// <typeparam name="T2">The second type to register as.</typeparam>
    /// <typeparam name="T3">The third type to register as.</typeparam>
    public abstract class Service<T1, T2, T3> : Service
    {
        protected override void Awake()
        {
            base.Awake();
            
            ObjectRegistry.Register<T1>((T1)(object)this);
            ObjectRegistry.Register<T2>((T2)(object)this);
            ObjectRegistry.Register<T3>((T3)(object)this);
        }

        protected virtual void OnDestroy()
        {
            ObjectRegistry.Unregister<T1>();
            ObjectRegistry.Unregister<T2>();
            ObjectRegistry.Unregister<T3>();
        }
    }
    
    /// <summary>
    /// A four arg generic boilerplate class that provides automatic self registration
    /// and cleanup.
    /// </summary>
    /// <typeparam name="T1">The first type to register as.</typeparam>
    /// <typeparam name="T2">The second type to register as.</typeparam>
    /// <typeparam name="T3">The third type to register as.</typeparam>
    /// <typeparam name="T4">The fourth type to register as.</typeparam>
    public abstract class Service<T1, T2, T3, T4> : Service
    {
        protected override void Awake()
        {
            base.Awake();
            
            ObjectRegistry.Register<T1>((T1)(object)this);
            ObjectRegistry.Register<T2>((T2)(object)this);
            ObjectRegistry.Register<T3>((T3)(object)this);
            ObjectRegistry.Register<T4>((T4)(object)this);
        }

        protected virtual void OnDestroy()
        {
            ObjectRegistry.Unregister<T1>();
            ObjectRegistry.Unregister<T2>();
            ObjectRegistry.Unregister<T3>();
            ObjectRegistry.Unregister<T4>();
        }
    }
    
    /// <summary>
    /// A five arg generic boilerplate class that provides automatic self registration
    /// and cleanup.
    /// </summary>
    /// <typeparam name="T1">The first type to register as.</typeparam>
    /// <typeparam name="T2">The second type to register as.</typeparam>
    /// <typeparam name="T3">The third type to register as.</typeparam>
    /// <typeparam name="T4">The fourth type to register as.</typeparam>
    /// <typeparam name="T5">The fifth type to register as.</typeparam>
    public abstract class Service<T1, T2, T3, T4, T5> : Service
    {
        protected override void Awake()
        {
            base.Awake();
            
            ObjectRegistry.Register<T1>((T1)(object)this);
            ObjectRegistry.Register<T2>((T2)(object)this);
            ObjectRegistry.Register<T3>((T3)(object)this);
            ObjectRegistry.Register<T4>((T4)(object)this);
            ObjectRegistry.Register<T5>((T5)(object)this);
        }

        protected virtual void OnDestroy()
        {
            ObjectRegistry.Unregister<T1>();
            ObjectRegistry.Unregister<T2>();
            ObjectRegistry.Unregister<T3>();
            ObjectRegistry.Unregister<T4>();
            ObjectRegistry.Unregister<T5>();
        }
    }
    
    /// <summary>
    /// A six arg generic boilerplate class that provides automatic self registration
    /// and cleanup.
    /// </summary>
    /// <typeparam name="T1">The first type to register as.</typeparam>
    /// <typeparam name="T2">The second type to register as.</typeparam>
    /// <typeparam name="T3">The third type to register as.</typeparam>
    /// <typeparam name="T4">The fourth type to register as.</typeparam>
    /// <typeparam name="T5">The fifth type to register as.</typeparam>
    /// <typeparam name="T6">The sixth type to register as.</typeparam>
    public abstract class Service<T1, T2, T3, T4, T5, T6> : Service
    {
        protected override void Awake()
        {
            base.Awake();
            
            ObjectRegistry.Register<T1>((T1)(object)this);
            ObjectRegistry.Register<T2>((T2)(object)this);
            ObjectRegistry.Register<T3>((T3)(object)this);
            ObjectRegistry.Register<T4>((T4)(object)this);
            ObjectRegistry.Register<T5>((T5)(object)this);
            ObjectRegistry.Register<T6>((T6)(object)this);
        }

        protected virtual void OnDestroy()
        {
            ObjectRegistry.Unregister<T1>();
            ObjectRegistry.Unregister<T2>();
            ObjectRegistry.Unregister<T3>();
            ObjectRegistry.Unregister<T4>();
            ObjectRegistry.Unregister<T5>();
            ObjectRegistry.Unregister<T6>();
        }
    }
    
    /// <summary>
    /// A seven arg generic boilerplate class that provides automatic self registration
    /// and cleanup.
    /// </summary>
    /// <typeparam name="T1">The first type to register as.</typeparam>
    /// <typeparam name="T2">The second type to register as.</typeparam>
    /// <typeparam name="T3">The third type to register as.</typeparam>
    /// <typeparam name="T4">The fourth type to register as.</typeparam>
    /// <typeparam name="T5">The fifth type to register as.</typeparam>
    /// <typeparam name="T6">The sixth type to register as.</typeparam>
    /// <typeparam name="T7">The seventh type to register as.</typeparam>
    public abstract class Service<T1, T2, T3, T4, T5, T6, T7> : Service
    {
        protected override void Awake()
        {
            base.Awake();
            
            ObjectRegistry.Register<T1>((T1)(object)this);
            ObjectRegistry.Register<T2>((T2)(object)this);
            ObjectRegistry.Register<T3>((T3)(object)this);
            ObjectRegistry.Register<T4>((T4)(object)this);
            ObjectRegistry.Register<T5>((T5)(object)this);
            ObjectRegistry.Register<T6>((T6)(object)this);
            ObjectRegistry.Register<T7>((T7)(object)this);
        }

        protected virtual void OnDestroy()
        {
            ObjectRegistry.Unregister<T1>();
            ObjectRegistry.Unregister<T2>();
            ObjectRegistry.Unregister<T3>();
            ObjectRegistry.Unregister<T4>();
            ObjectRegistry.Unregister<T5>();
            ObjectRegistry.Unregister<T6>();
            ObjectRegistry.Unregister<T7>();
        }
    }
    
    /// <summary>
    /// An eight arg generic boilerplate class that provides automatic self registration
    /// and cleanup.
    /// </summary>
    /// <typeparam name="T1">The first type to register as.</typeparam>
    /// <typeparam name="T2">The second type to register as.</typeparam>
    /// <typeparam name="T3">The third type to register as.</typeparam>
    /// <typeparam name="T4">The fourth type to register as.</typeparam>
    /// <typeparam name="T5">The fifth type to register as.</typeparam>
    /// <typeparam name="T6">The sixth type to register as.</typeparam>
    /// <typeparam name="T7">The seventh type to register as.</typeparam>
    /// <typeparam name="T8">The eighth type to register as.</typeparam>
    public abstract class Service<T1, T2, T3, T4, T5, T6, T7, T8> : Service
    {
        protected override void Awake()
        {
            base.Awake();
            
            ObjectRegistry.Register<T1>((T1)(object)this);
            ObjectRegistry.Register<T2>((T2)(object)this);
            ObjectRegistry.Register<T3>((T3)(object)this);
            ObjectRegistry.Register<T4>((T4)(object)this);
            ObjectRegistry.Register<T5>((T5)(object)this);
            ObjectRegistry.Register<T6>((T6)(object)this);
            ObjectRegistry.Register<T7>((T7)(object)this);
            ObjectRegistry.Register<T8>((T8)(object)this);
        }

        protected virtual void OnDestroy()
        {
            ObjectRegistry.Unregister<T1>();
            ObjectRegistry.Unregister<T2>();
            ObjectRegistry.Unregister<T3>();
            ObjectRegistry.Unregister<T4>();
            ObjectRegistry.Unregister<T5>();
            ObjectRegistry.Unregister<T6>();
            ObjectRegistry.Unregister<T7>();
            ObjectRegistry.Unregister<T8>();
        }
    }
}