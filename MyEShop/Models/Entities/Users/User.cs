using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace MyEShop.Models.Entities.Users
{
    public class User :BaseEntity
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string? PostalCode { get; set; }
        public string Password { get; set; }

    }
}
