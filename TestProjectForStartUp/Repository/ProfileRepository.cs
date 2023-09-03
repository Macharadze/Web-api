using Microsoft.AspNetCore.Http.HttpResults;
using TestProjectForStartUp.ExtraHelp;
using TestProjectForStartUp.Interfaces;
using ZauriStartUp.Models;

namespace TestProjectForStartUp.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly MyDB context;
        public ProfileRepository(MyDB db)
        {
            context = db;
        }

        public bool CreateProfile(Profile profile)
        {
            context.Add(profile);
            return Save();
        }

        public bool Exist(int id) => context.profiles.Any(a => a.Id == id);

        public ICollection<Comment> GetComments(int profId) => context.comments.Where(a=>a.Profile.Id == profId).ToList();

        public Account GetOwner(int profId) => context.accounts
            .Where(a=>a.Profiles.Any(p=>p.Id == profId)).FirstOrDefault();
        
      

        public ProfileHelp GetProfile(int id)
        {
            var acc = context.profiles.Where(a => a.Id == id).FirstOrDefault();
            var pro = new ProfileHelp
            {
                Name = acc.Name,
                LastName = acc.LastName,
                Email = acc.Email,
                about = acc.about,
                Phone = acc.Phone,
                Image = acc.Image,
                Address = acc.Address,
                Profession = acc.Profession,
                userId = acc.userId,
            };
            return pro;

        }

        public ICollection<Profile> GetProfiles() => context.profiles.OrderBy(a => a.Id).ToList();

        public Profile ProfuleHelp(int profId) => context.profiles.Where(x=>x.Id == profId).FirstOrDefault();

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
