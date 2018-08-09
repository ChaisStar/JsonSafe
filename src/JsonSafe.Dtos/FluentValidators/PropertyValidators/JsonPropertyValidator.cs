namespace JsonSafe.Dtos.FluentValidators.PropertyValidators
{
    using System;
    using FluentValidation.Validators;
    using Infrastructure.Constants;
    using Newtonsoft.Json.Linq;

    public class JsonPropertyValidator: PropertyValidator
    {
        public JsonPropertyValidator() : base(StringConstants.ValidationMessages.InvalidJson)
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            try
            {
                return JToken.Parse(context.PropertyValue as string).HasValues;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
