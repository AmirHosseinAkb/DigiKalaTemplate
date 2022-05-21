using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Data.Entities.User
{
    public class User
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BirthDate { get; set; }
        public byte? RefundType { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string AvatarName { get; set; } = "Default.png";
        public string ActivationCode { get; set; }
        public bool IsActive { get; set; } 
    }
}
