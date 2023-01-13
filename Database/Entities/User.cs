using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAccounts.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; } = string.Empty;

        [Required]
        public int PersonId { get; set; }

        // FOREIGNS

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; } = default!;
    }
}