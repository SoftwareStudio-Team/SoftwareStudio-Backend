using Backend.Models;

namespace Backend.Services
{
    public interface IAccountService
    {
        List<Account> GetAll();

        Account? GetById(string id);

        Account? GetByUsername(string username);

        Account Create(Account account);

        void Update(string id, Account account);

        void Remove(string id);
    }
}