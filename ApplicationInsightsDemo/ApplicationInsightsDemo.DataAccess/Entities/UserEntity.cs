namespace ApplicationInsightsDemo.DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}