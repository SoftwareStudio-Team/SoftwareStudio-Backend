using Backend.DTOs;
using Backend.Models;

namespace Backend.Services
{
    public interface IAccountService
    {
        Account? GetById(string id);

        List<AccountDTO> GetAllDTO();

        AccountDTO? GetDTOById(string id);

        Account? GetAccountByUsername(string username);

        Account Create(Account account);

        void Update(string id, Account account);

        void Remove(string id);
    }
}