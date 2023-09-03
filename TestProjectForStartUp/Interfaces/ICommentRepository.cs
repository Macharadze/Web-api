using ZauriStartUp.Models;

namespace TestProjectForStartUp.Interfaces
{
    public interface ICommentRepository
    {
        bool Exist(int id);
        Comment GetComment(int id);
        ICollection<Comment> GetComments();
        bool Save();
        bool CreateComment(Comment comment);
    }
}
