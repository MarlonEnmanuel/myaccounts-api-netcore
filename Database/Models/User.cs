using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyAccounts.Database.Models
{
    public class User
    {
        #region Attributes

        [Key]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        #endregion

        #region Foreigns

        [JsonIgnore]
        public virtual ICollection<Person> Persons { get; set; } = default!;

        #endregion

        #region Others

        public Person? Person => Persons.FirstOrDefault(p => p.IsUser);

        public User(string key)
        {
            Key = key;
        }

        #endregion
    }
}