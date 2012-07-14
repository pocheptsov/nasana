using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace NAsana.API.v1.Exceptions
{
    /// <summary>
    /// Represent errors that occur while calling a Asana API.
    /// </summary>
#if !(SILVERLIGHT || NETFX_CORE)
    [Serializable]
#endif
    public class NAsanaApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NAsanaApiException"/> class.
        /// </summary>
        public NAsanaApiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NAsanaApiException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NAsanaApiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NAsanaApiException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorType">Type of the error.</param>
        public NAsanaApiException(string message, string errorType)
            : this(String.Format(CultureInfo.InvariantCulture, "({0}) {1}", errorType ?? "Unknown", message))
        {
            ErrorType = errorType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NAsanaApiException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="errorCode">Code of the error.</param>
        public NAsanaApiException(string message, string errorType, int errorCode)
            : this(
                String.Format(CultureInfo.InvariantCulture, "({0} - #{1}) {2}", errorType ?? "Unknown", errorCode,
                              message))
        {
            ErrorType = errorType;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NAsanaApiException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NAsanaApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !(SILVERLIGHT || NETFX_CORE)
        /// <summary>
        /// Initializes a new instance of the <see cref="NAsanaApiException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected NAsanaApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

        /// <summary>
        /// Gets or sets the type of the error.
        /// </summary>
        /// <value>The type of the error.</value>
        public string ErrorType { get; set; }

        /// <summary>
        /// Gets or sets the code of the error.
        /// </summary>
        /// <value>The code of the error.</value>
        public int ErrorCode { get; set; }
    }
}