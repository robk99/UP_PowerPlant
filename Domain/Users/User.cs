
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Users
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public User(string email, string passwordHash, string? firstName, string? lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
        }

        // Parameterless constructor for EF Core
        private User() { }
    }
}
