namespace JsonSafe.Infrastructure.Exceptions.BusinessLogicExceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using Constants;

    [Serializable]
    public class InvalidCredentialsException : BaseBusinessLogicException
    {
        public InvalidCredentialsException() : base(StringConstants.BusinessLogicExceptionMessages.InvalidCredentials)
        {
        }

        [ExcludeFromCodeCoverage]
        protected InvalidCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
