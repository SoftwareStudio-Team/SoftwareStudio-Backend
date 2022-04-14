using MongoDB.Driver;

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

        public List<Account> GetAll()
        {
            return this._accounts.Find(element => true).ToList();
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

        public Account? GetByUsername(string username)
        {
            try
            {
                return this._accounts.Find(element => element.Username == username).FirstOrDefault();
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