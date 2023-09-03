using Microsoft.EntityFrameworkCore;
using TestProjectForStartUp.Interfaces;
using ZauriStartUp.Models;

namespace TestProjectForStartUp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDB context;
        public AccountRepository(MyDB db)
        {
            context = db;
        }

        public bool CreateAccount(Account account)
        {
            context.Add(account);
            return Save();
        }

        public bool Exist(int id) =>context.accounts.Any(a => a.Id == id);

        public Account GetAccount(int id) => context.accounts.Where(a => a.Id == id).FirstOrDefault();

        public ICollection<Account> GetAccounts()=>context.accounts.OrderBy(a => a.Id).ToList();

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
