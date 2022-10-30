
using System.ComponentModel;

namespace DotNet6API.Models.Users
{
    public class UserModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Cdate { get; set; }
        public bool Confirmed { get; set; }
    }
    public class Login 
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
    public class Register : UserModel
    {
        public string UserName { get; set; } = string.Empty;
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
