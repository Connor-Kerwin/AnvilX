using System;

namespace AnvilX
{
    /// <summary>
    /// Facilitates automatic source generation for a dependency.
    /// Use this to auto-generate a lazy-accessor property on your type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// The type of the generated property.
        /// </summary>
        public Type Type { get; }
        
        /// <summary>
        /// The type to resolve the dependency as.
        /// Use this to define a custom interface to resolve by, rather than the concrete type.
        /// </summary>
        public Type ResolveAs { get; set; }
        
        /// <summary>
        /// Whether the dependency is optional.
        /// </summary>
        public bool Optional { get; set; }
        
        /// <summary>
        /// The custom name to give to the generated property.
        /// </summary>
        public string CustomPropertyName { get; set; }
        
        /// <summary>
        /// The access modifier given to the generated property.
        /// </summary>
        public AccessModifierLevel AccessModifierLevel { get; set; }

        /// <summary>
        /// Define a dependency, defaulting all of its properties.
        /// </summary>
        /// <remarks>
        /// Defaults to a required dependency, resolved by its own type, exposed publicly, named the same as the type.
        /// </remarks>
        /// <param name="type">The type of the generated property.</param>
        public DependsOnAttribute(Type type)
        {
            Type = type;
            ResolveAs = type;
            Optional = false;
            CustomPropertyName = null;
            AccessModifierLevel = AccessModifierLevel.Public;
        }
    }

    public enum AccessModifierLevel
    {
        Public = 0,
        Protected = 1,
        Internal = 2,
        Private = 3,
    }
}