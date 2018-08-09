namespace JsonSafe.DbModels
{
    using System;

    public class DbUser
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string Email { get; set; }

        public string ApiKey { get; set; }
    }
}
