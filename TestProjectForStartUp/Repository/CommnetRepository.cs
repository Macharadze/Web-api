using System.Windows.Input;
using TestProjectForStartUp.Interfaces;
using ZauriStartUp.Models;

namespace TestProjectForStartUp.Repository
{
    public class CommnetRepository : ICommentRepository
    {
        private readonly MyDB myDB;
        public CommnetRepository(MyDB myDB)
        {
            this.myDB = myDB;
        }
        public bool CreateComment(Comment comment)
        {
            myDB.Add(comment);
            return Save();
        }

        public bool Exist(int id)=>myDB.comments.Any(a => a.CommetId == id);

        public Comment GetComment(int id) => myDB.comments.Where(a => a.CommetId == id).FirstOrDefault();

        public ICollection<Comment> GetComments() => myDB.comments.OrderBy(a=>a.CommetId).ToList();

        public bool Save()
        {
            var saved = myDB.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
