using System;
using System.ComponentModel;

namespace Babaganoush.Core.Extensions
{
    /// <summary>
    /// Extension methods for .NET objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Changes the type of the given <paramref name="value"/> to the specified
        /// <typeparamref name="TDestinationType"/>.
        /// </summary>
        ///
        /// <exception cref="InvalidCastException">Thrown when attempting to convert to an incompatible
        /// type (e.g. "abc" to int).</exception>
        ///
        /// <tparam name="TDestinationType">
        /// Type of the destination type.
        /// </tparam>
        /// <param name="value">The object to change.</param>
        ///
        /// <returns>
        /// The given object converted to an instance of <typeparamref name="TDestinationType"/>.
        /// </returns>
        ///
        /// ### <typeparam name="TDestinationType">The type to change to.</typeparam>
        public static TDestinationType ChangeTypeTo<TDestinationType>(this object value)
        {
            if (value != null && typeof(TDestinationType) == value.GetType())
            {
                return (TDestinationType)value;
            }

            Type destinationType = typeof(TDestinationType);
            if (destinationType.IsNullableType())
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return (TDestinationType)(object)null;
                }
                var nullableConverter = new NullableConverter(destinationType);
                destinationType = nullableConverter.UnderlyingType;
            }

            try
            {
                return (TDestinationType)Convert.ChangeType(value, destinationType);
            }
            catch (FormatException originalException)
            {
                string exceptionMessage = string.Format("Cannot cast element value '{0}' to type '{1}'.",
                    value, typeof(TDestinationType).Name);
                throw new InvalidCastException(exceptionMessage, originalException);
            }
        }
    }
}