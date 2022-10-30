
using DotNet6API.Logics.Users;

using Microsoft.EntityFrameworkCore;

namespace DotNet6API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static DataContext _dbContext;
        public static IConfiguration _configuration;
        public static User user = new User();
        public static Helper helper = new Helper();
        public static UserLogic userLogic = new UserLogic();
        public UsersController(IConfiguration Configuration,DataContext Context)
        {
            _configuration = Configuration;
            _dbContext = Context;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterAsync(Register req)
        {
            return await userLogic.Register(req, _dbContext);           
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(Login req)
        {
            if (req.Password != req.ConfirmPassword) {return BadRequest("ConfirmPassword not matched.");}
            user = _dbContext.Users.FirstOrDefault(x => x.UserName == req.UserName);
            if(user == null){ return BadRequest("User not found.");}         
            if(!helper.VerifyPasswordHash(req.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            byte[] appSetting = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var token = helper.GenerateToken(user, appSetting);
            var response = string.Format("Token: {0}", token);
            return Ok(response);
        }

        [HttpGet("AllUsers")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }
    }
}
