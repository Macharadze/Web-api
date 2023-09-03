using TestProjectForStartUp.ExtraHelp;
using ZauriStartUp.Models;

namespace TestProjectForStartUp.Interfaces
{
    public interface IProfileRepository
    {
        bool Exist(int id);
        ProfileHelp GetProfile(int id);
        ICollection<Profile> GetProfiles();
        bool Save();
        bool CreateProfile(Profile profile);
        Account GetOwner(int profId);
        ICollection<Comment> GetComments(int profId);
        Profile ProfuleHelp(int profId);
    }
}
