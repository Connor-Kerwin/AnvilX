#nullable enable

using System;

namespace AnvilX
{
    public interface IObjectResolver
    {
        object? Resolve(Type type);
    }
    
    public static class ObjectResolverExtensions
    {
        public static T? Resolve<T>(this IObjectResolver resolver)
            where T : class
        {
            return resolver.Resolve(typeof(T)) as T;
        }

        public static T ResolveRequired<T>(this IObjectResolver resolver)
            where T : class
        {
            var instance = resolver.Resolve(typeof(T)) as T;
            if (instance == null)
            {
                throw new Exception($"Failed to resolve required dependency '{nameof(T)}'");
            }

            return instance;
        }
    }
}