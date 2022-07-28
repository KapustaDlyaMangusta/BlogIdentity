using BlogIdentity.Dtos;
using BlogIdentity.EntityFramework;
using BlogIdentity.Models;
using BlogIdentity.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BlogIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public IdentityController(AppDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if (await UserExists(registerDto.Login)) return BadRequest("Login Is Already Taken");

            var user = new User
            {
                Login = registerDto.Login,
                Password = registerDto.Password

            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Login = user.Login,
                Token = _tokenService.CreateToken(user)
            };
        }



        private async Task<bool> UserExists(string login)
        {
            return await _dbContext.Users.AnyAsync(x => x.Login == login);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Login == loginDto.Login);

            if (user == null) return Unauthorized("Invalid Login");

            var pass = loginDto.Password;

            for (int i = 0; i < pass.Length; i++)
            {
                if (pass[i] != user.Password[i]) return Unauthorized("Invalid Password");
            }

            return new UserDto
            {
                Login = user.Login,
                Token = _tokenService.CreateToken(user)
            };


        }
    }
}
