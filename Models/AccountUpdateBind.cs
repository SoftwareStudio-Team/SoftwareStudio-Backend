using Microsoft.AspNetCore.Mvc;

namespace Backend.Models
{
    [ModelMetadataType(typeof(Account))]
    public class AccountUpdateBind
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}