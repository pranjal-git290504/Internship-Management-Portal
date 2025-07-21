using System.Text.Json.Serialization;

namespace Intern.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]
        public int FKRoleId { get; set; }
        [JsonIgnore]
        public string? PasswordHash { get; set; }
        [JsonIgnore]
        public string RoleName { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public string? ResetToken { get; set; }
        [JsonIgnore]
        public DateTime? ResetTokenExpiry { get; set; }

        public User()
        {
            FKRoleId = 2; // By Default User Role
        }
    }
}
