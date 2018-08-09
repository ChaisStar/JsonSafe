namespace JsonSafe.Infrastructure.Exceptions.BusinessLogicExceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class BaseBusinessLogicException : Exception
    {
        protected BaseBusinessLogicException(string message) : base(message)
        {
        }

        [ExcludeFromCodeCoverage]
        protected BaseBusinessLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}