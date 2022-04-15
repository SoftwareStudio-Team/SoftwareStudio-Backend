using MongoDB.Driver;

using Backend.DTOs;
using Backend.Models;

namespace Backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountService(IDatabaseSettings databaseSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            this._accounts = database.GetCollection<Account>(databaseSettings.AccountsCollectionName);
        }

        public Account? GetById(string id)
        {
            try
            {
                return this._accounts.Find(element => element.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<AccountDTO> GetAllDTO()
        {
            return this._accounts.Find(element => true).ToList().Select(element => new AccountDTO()
            {
                Id = element.Id,
                Username = element.Username,
                FirstName = element.FirstName,
                LastName = element.LastName,
                BirthDate = element.BirthDate,
                Role = element.Role,
                IsBanned = element.IsBanned,
            }).ToList();
        }

        public AccountDTO? GetDTOById(string id)
        {
            try
            {
                var account = this._accounts.Find(element => element.Id == id).FirstOrDefault();
                return new AccountDTO()
                {
                    Id = account.Id,
                    Username = account.Username,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    BirthDate = account.BirthDate,
                    Role = account.Role,
                    IsBanned = account.IsBanned,
                };
            }
            catch
            {
                return null;
            }
        }

        public string? GetPasswordByUsername(string username)
        {
            try
            {
                return this._accounts.Find(element => element.Username == username).ToList().Select(element => element.Password).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public Account Create(Account account)
        {
            this._accounts.InsertOne(account);
            return account;
        }

        public void Update(string id, Account account)
        {
            this._accounts.ReplaceOne(element => element.Id == id, account);
        }

        public void Remove(string id)
        {
            this._accounts.DeleteOne(element => element.Id == id);
        }
    }
}