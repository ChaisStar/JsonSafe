namespace JsonSafe.Infrastructure.Exceptions.BusinessLogicExceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using Constants;

    [Serializable]
    public class EmailExistException : BaseBusinessLogicException
    {
        public EmailExistException() : base(StringConstants.BusinessLogicExceptionMessages.EmailExist)
        {
        }

        [ExcludeFromCodeCoverage]
        protected EmailExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
