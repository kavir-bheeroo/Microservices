using System;

namespace Revenue.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    [System.Serializable]
    public class RevenueDomainException : Exception
    {
        public RevenueDomainException() { }
        public RevenueDomainException(string message) : base(message) { }
        public RevenueDomainException(string message, Exception inner) : base(message, inner) { }
        protected RevenueDomainException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}