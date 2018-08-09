namespace JsonSafe.Services.Infrastructure
{
    using System;

    public class ApiKeyGenerator : IApiKeyGenerator
    {
        private readonly INumberGenerator _numberGenerator;

        public ApiKeyGenerator(INumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator;
        }

        public string GenerateNewApiKey()
        {
            return Convert.ToBase64String(_numberGenerator.GetBytes());
        }
    }
}