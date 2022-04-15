using Microsoft.AspNetCore.Mvc;

using Backend.Models;

namespace Backend.DTOs
{
    [ModelMetadataType(typeof(Account))]
    public class AccountDTO
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Role { get; set; }

        public bool IsBanned { get; set; }
    }
}

