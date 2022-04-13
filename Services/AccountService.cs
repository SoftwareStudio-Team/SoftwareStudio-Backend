using MongoDB.Driver;
using System.Security.Cryptography;

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

        public Account Get(string id)
        {
            return this._accounts.Find(element => element.Id == id).FirstOrDefault();
        }

        public Account Create(Account account)
        {
            this._accounts.InsertOne(account);
            return account;
        }

        public void Update(string id, Account account)
        {
            account.Id = id;
            this._accounts.ReplaceOne(element => element.Id == id, account);
        }

        public void Remove(string id)
        {
            this._accounts.DeleteOne(element => element.Id == id);
        }
    }
}