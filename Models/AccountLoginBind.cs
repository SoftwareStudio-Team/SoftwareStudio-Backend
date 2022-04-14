using Microsoft.AspNetCore.Mvc;

namespace Backend.Models
{
    [ModelMetadataType(typeof(Account))]
    public class AccountLoginBind
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}