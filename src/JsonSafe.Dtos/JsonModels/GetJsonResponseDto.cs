namespace JsonSafe.Dtos.JsonModels
{
    using System;

    public class GetJsonResponseDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string Name { get; set; }

        public string Json { get; set; }
    }
}
