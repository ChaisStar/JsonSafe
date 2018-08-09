namespace JsonSafe.Dtos.FluentValidators.Extensions
{
    using FluentValidation;
    using PropertyValidators;

    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeCorrectJson<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new JsonPropertyValidator());
        }
    }
}
