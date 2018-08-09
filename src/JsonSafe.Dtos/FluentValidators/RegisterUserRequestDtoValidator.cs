namespace JsonSafe.Dtos.FluentValidators
{
    using FluentValidation;
    using UserModels;

    public class RegisterUserRequestDtoValidator : NullCheckingAbstractValidator<RegisterUserRequestDto>
    {
        private const int MinimumLength = 5;
        private const int MaximumEmailLength = 100;
        private const int MaximumUsernameLength = 50;

        public RegisterUserRequestDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(MinimumLength, MaximumEmailLength);
            RuleFor(x => x.Username).NotEmpty().Length(MinimumLength, MaximumUsernameLength);
            RuleFor(x => x.Password).NotEmpty().Length(MinimumLength, int.MaxValue);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}
