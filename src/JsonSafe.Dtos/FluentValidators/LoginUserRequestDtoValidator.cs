namespace JsonSafe.Dtos.FluentValidators
{
    using FluentValidation;
    using UserModels;

    public class LoginUserRequestDtoValidator : NullCheckingAbstractValidator<LoginUserRequestDto>
    {
        public LoginUserRequestDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
