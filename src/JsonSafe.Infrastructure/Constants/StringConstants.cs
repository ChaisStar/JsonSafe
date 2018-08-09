namespace JsonSafe.Infrastructure.Constants
{
    public static class StringConstants
    {
        public static class BusinessLogicExceptionMessages
        {
            public const string UsernameExist = "Username is already exists";
            public const string EmailExist = "Email is already exists";
            public const string InvalidCredentials = "Invalid username or password";
        }
        public static class ExceptionMessages
        {
            public const string CannotCreateUser = "Cannot create user";
        }
        public static class ValidationMessages
        {
            public const string InvalidJson = "Invalid Json";
        }
    }
}
