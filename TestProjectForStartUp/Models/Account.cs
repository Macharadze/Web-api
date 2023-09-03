using System.ComponentModel.DataAnnotations;

namespace ZauriStartUp.Models  
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public ICollection<Profile> Profiles { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
