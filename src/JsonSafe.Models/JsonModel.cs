namespace JsonSafe.Models
{
    using System;

    public class JsonModel
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string Name { get; set; }

        public string Json { get; set; }
    }
}
