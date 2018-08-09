namespace JsonSafe.Infrastructure.Exceptions.BusinessLogicExceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using Constants;

    [Serializable]
    public class UsernameExistException : BaseBusinessLogicException
    {
        public UsernameExistException() : base(StringConstants.BusinessLogicExceptionMessages.UsernameExist)
        {
        }

        [ExcludeFromCodeCoverage]
        protected UsernameExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
