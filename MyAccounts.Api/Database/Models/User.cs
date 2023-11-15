using MyAccounts.Api.Database.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MyAccounts.Api.Database.Models
{
    public class User : IIdentity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        // foreigns

        public List<Person>? Persons { get; set; }
    }
}