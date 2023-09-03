using System.ComponentModel.DataAnnotations;

namespace ZauriStartUp.Models
{
    public class Profile {
        [Key]
          public int Id { get; set; }
    public string Profession { get; set; }

    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string about { get; set; }
    public string DateTime { get; set; }
    public int Comments { get; set; } 
    public string Image { get; set; }
    public int userId { get; set; }
    public  Account User { get; set; }
    public  ICollection<Comment> comments { get; set; }
    
    }
}
