using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyAccounts.Database.Models
{
    public class Person
    {
        #region Attributes

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool IsUser { get; set; }

        [Required]
        public bool IsShared { get; set; }

        #endregion

        #region Foreigns

        [JsonIgnore]
        public virtual User User { get; set; } = default!;

        [JsonIgnore]
        public virtual ICollection<Card> Cards { get; set; } = default!;

        [JsonIgnore]
        public virtual ICollection<PaymentSplit> PaymentSplits { get; set; } = default!;

        #endregion

        #region Others

        public Person(string name, int userId, bool isUser, bool isShared)
        {
            Name = name;
            UserId = userId;
            IsUser = isUser;
            IsShared = isShared;
        }

        #endregion
    }
}
