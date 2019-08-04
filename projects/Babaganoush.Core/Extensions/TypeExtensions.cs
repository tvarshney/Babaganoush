using System;

namespace Babaganoush.Core.Extensions
{
    /// <summary>
    /// Extensions for the .NET Type class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks the given type and determines whether or not it is nullable.
        /// </summary>
        ///
        /// <param name="value">The type to test.</param>
        ///
        /// <returns>
        /// <c>True</c> if the given <paramref name="value"/> is nullable. <c>False</c> otherwise.
        /// </returns>
        public static bool IsNullableType(this Type value)
        {
            if (value == null || !value.IsGenericType)
            {
                return false;
            }
            Type genericTypeDefinition = value.GetGenericTypeDefinition();
            Type nullableType = typeof(Nullable<>);
            return genericTypeDefinition != null && genericTypeDefinition == nullableType;
        }

        /// <summary>
        /// To Implement Later
        /// ** Sitefinitysteve.com Extension **.
        /// </summary>
        ///
        /// <param name="value">.</param>
        ///
        /// <returns>
        /// true if simple type, false if not.
        /// </returns>
        public static bool IsSimpleType(this Type value)
        {
            return value == typeof(String) || value.IsValueType || value.IsPrimitive || Convert.GetTypeCode(value) != TypeCode.Object;
        }
    }
}