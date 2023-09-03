using System.ComponentModel.DataAnnotations;

namespace ZauriStartUp.Models
{
    public class Comment
    {
        [Key] 
        public int CommetId { get; set; }
        public string Body { get; set; }
        public int AccountId { get; set; } // Foreign key property for Account
        public int ProfileId { get; set; } // Foreign key property for Profile
        public Account Account { get; set; }
        public Profile Profile { get; set; }
        
    }
}
