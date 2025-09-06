using System;

namespace AnvilX
{
    public static class ParameterValidation
    {
        /// <summary>
        /// Throw an <see cref="ArgumentNullException"/> if <paramref name="target"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="target">The parameter that is being validated.</param>
        /// <param name="parameterName">The name of the parameter that is being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is considered as <see langword="null"/>.</exception>
        public static void ThrowIfNull(UnityEngine.Object target, string parameterName)
        {
            if (!target)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throw an <see cref="ArgumentNullException"/> if <paramref name="target"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="target">The parameter that is being validated.</param>
        /// <param name="parameterName">The name of the parameter that is being validated.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is considered as <see langword="null"/>.</exception>
        public static void ThrowIfNull(object target, string parameterName)
        {
            if (target == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
