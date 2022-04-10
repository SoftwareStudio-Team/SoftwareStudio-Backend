using Backend.Models;

namespace Backend.Services
{
    public interface IAccountService
    {
        Account Get(string id);

        Account Create(Account account);

        void Update(string id, Account account);

        void Remove(string id);
    }
}