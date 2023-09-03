using ZauriStartUp.Models;

namespace TestProjectForStartUp.Interfaces
{
    public interface IAccountRepository
    {
        bool Exist(int id);
        Account GetAccount(int id);
        ICollection<Account> GetAccounts();
        bool Save();
        bool CreateAccount(Account account);

    }
}
