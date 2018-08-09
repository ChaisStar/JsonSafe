namespace JsonSafe.Dtos.FluentValidators
{
    using FluentValidation;
    using FluentValidation.Results;

    public class NullCheckingAbstractValidator<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            return context.InstanceToValidate == null
                ? new ValidationResult(new[] { new ValidationFailure("Request", "Instance cannot be null") })
                : base.Validate(context);
        }
    }
}
