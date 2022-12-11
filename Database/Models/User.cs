using System.ComponentModel.DataAnnotations;

namespace MyAccounts.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; } = string.Empty;

        // FOREIGNS

        public virtual ICollection<Person> Persons { get; set; } = Array.Empty<Person>();
    }
}