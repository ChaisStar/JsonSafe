namespace JsonSafe.Dtos.FluentValidators
{
    using Extensions;
    using FluentValidation;
    using JsonModels;

    public class CreateJsonRequestDtoValidator : NullCheckingAbstractValidator<CreateJsonRequestDto>
    {
        public CreateJsonRequestDtoValidator()
        {
            RuleFor(x => x.Json).NotEmpty().MustBeCorrectJson();
        }
    }
}
