namespace NAsana.API.v1.Utils
{
    using System;

    public class Guard
    {
        /// <summary>
        /// Throws an exception if the string is empty or null.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown if value is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if value is null.</exception>
        internal static void NotNullOrEmpty(string paramName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(SR.ArgumentEmptyError, paramName);
            }
        }

        /// <summary>
        /// Throw an exception if the value is null.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if value is null.</exception>
        public static void NotNull(string paramName, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throw an exception indicating argument is out of range.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        internal static void ArgumentOutOfRange(string paramName, object value)
        {
            throw new ArgumentOutOfRangeException(paramName, value, SR.ArgumentOutOfRangeError);
        }

        /// <summary>
        /// Throw an exception if the argument is out of bounds.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="val">The value of the parameter.</param>
        /// <param name="min">The minimum value for the parameter.</param>
        /// <param name="max">The maximum value for the parameter.</param>
        internal static void InBounds<T>(string paramName, T val, T min, T max)
            where T : IComparable
        {
            if (val.CompareTo(min) < 0)
            {
                throw new ArgumentOutOfRangeException(String.Format(SR.ArgumentTooSmallError, paramName, min), paramName);
            }

            if (val.CompareTo(max) > 0)
            {
                throw new ArgumentOutOfRangeException(String.Format(SR.ArgumentTooLargeError, paramName, max), paramName);
            }
        }

        public static void LowerThan<T>(string paramName, T val, T min)
            where T : IComparable
        {
            if (val.CompareTo(min) > 0)
            {
                throw new ArgumentOutOfRangeException(String.Format(SR.ArgumentTooSmallError, paramName, min), paramName);
            }
        }
        
        public static void GreaterThan<T>(string paramName, T val, T max)
            where T : IComparable
        {
            if (val.CompareTo(max) < 0)
            {
                throw new ArgumentOutOfRangeException(String.Format(SR.ArgumentTooSmallError, paramName, max), paramName);
            }
        }

        public static void NotEqual<T>(string paramName, T val1, T val2)
            where T : IComparable
        {
            if (val1.CompareTo(val2) != 0)
            {
                throw new ArgumentException(String.Format(SR.ArgumentNotEqual, paramName, val1, val2), paramName);
            }
        }
        
        public static void Equal<T>(string paramName, T val1, T val2)
            where T : IComparable
        {
            if (val1.CompareTo(val2) == 0)
            {
                throw new ArgumentException(String.Format(SR.ArgumentEqual, paramName, val1, val2), paramName);
            }
        }
    }
}