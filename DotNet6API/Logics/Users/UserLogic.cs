using DotNet6API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotNet6API.Logics.Users
{
    public class UserLogic 
    {
        public static User user = new User();
        public static Helper _helper = new Helper();
       
        public Task<User> Register(Register model, DataContext dbContext)
        {
            try
            {
                _helper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Gender = model.Gender;
                user.UserName = model.UserName;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Cdate = model.Cdate;
                user.Confirmed = model.Confirmed;
                dbContext.Add(user);
                dbContext.SaveChanges();
                return Task.FromResult(user);
            }
            catch (Exception ex)
            {
                return Task.FromException<User>(ex);
            }
        }
       
    }
}
