

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiATON.Features.DtoModels;
using WebApiATON.Features.ManagersInterfaces;

namespace WebApiATON.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserManager userManager;
        public UsersController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("CreateUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromQuery] UserDto userDto)
        {
            var currentUserLogin = User.Identity?.Name;
            if (string.IsNullOrEmpty(currentUserLogin))
            {
                return Unauthorized("Не удалось определить создателя");
            }
            if (await userManager.isReg(userDto.Login))
            {
                BadRequest("Пользователь с таким логином существует");
            }
            var user = await userManager.CreateUser(userDto, currentUserLogin);
            return Ok(user);

        }
        [Authorize]
        [HttpPut("UpdateUserInformation")]
        public async Task<IActionResult> UpdateUserInformation([FromQuery] UpdateUserRequest request)
        {
            var currentLogin = User.Identity?.Name;
            var isAdmin = User.IsInRole("Admin");
            string targetLogin = null; ;
            if (isAdmin)
            {
                if (string.IsNullOrEmpty(request.Login))
                {
                    targetLogin = currentLogin;
                }
                else
                {
                    targetLogin = request.Login;
                }
            }
            else
            {
                
                if (!string.IsNullOrEmpty(request.Login) && request.Login != currentLogin)
                    return BadRequest("Пользователь не может изменять другие аккаунты");
                targetLogin = currentLogin;
            }
            
            var user = await userManager.GetUser(targetLogin);
            if (user == null)
                return NotFound("User not found");

            if (!isAdmin && user.RevokedOn != null)
                return Forbid("User is not active");
            
            return Ok(await userManager.UppdateUserInformation(user, request, currentLogin));
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromQuery] ChangePasswordRequest request)
        {
            var currentLogin = User.Identity?.Name;
            var isAdmin = User.IsInRole("Admin");
            string targetLogin = null; ;
            if (isAdmin)
            {
                if (string.IsNullOrEmpty(request.Login))
                {
                    targetLogin = currentLogin;
                }
                else
                {
                    targetLogin = request.Login;
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(request.Login) && request.Login != currentLogin)
                    return BadRequest("Пользователь не может изменять другие аккаунты");
                targetLogin = currentLogin;
            }
            var user = await userManager.GetUser(targetLogin);
            if (user == null)
                return NotFound("User not found");

            if (!isAdmin && user.RevokedOn != null)
                return Forbid("User is not active");
            return Ok(await userManager.ChangePassword(user, request, currentLogin));
        }

        [Authorize]
        [HttpPut("ChangeLogin")]
        public async Task<IActionResult> ChangeLogin([FromQuery] ChangeLoginRequest request)
        {
            var currentLogin = User.Identity?.Name;
            var isAdmin = User.IsInRole("Admin");
            string targetLogin = null; ;
            if (isAdmin)
            {
                if (string.IsNullOrEmpty(request.Login))
                {
                    targetLogin = currentLogin;
                }
                else
                {
                    targetLogin = request.Login;
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(request.Login) && request.Login != currentLogin)
                    return BadRequest("Пользователь не может изменять другие аккаунты");
                targetLogin = currentLogin;
            }
            var user = await userManager.GetUser(targetLogin);
            if (user == null)
                return NotFound("User not found");
            if (!isAdmin && user.RevokedOn != null)
                return Forbid("User is not active");

            if (await userManager.isReg(request.NewLogin))
            {
                return BadRequest("Пользователь с таким логином существует");
            }
            return Ok(await userManager.ChangeLogin(user, request, currentLogin));
        }


        [HttpGet("active")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetActiveUsers()
        {
            var users = await userManager.GetActiveUsersAsync();
            return Ok(users);
        }

        [HttpGet("by-login/{login}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByLogin(string login)
        {
            var user = await userManager.GetUserInfoByLoginAsync(login);
            if (user == null) return NotFound("User not found"); ;
            return Ok(user);
        }

        [HttpPost("SelfInfo")]
        public async Task<IActionResult> GetSelfInfo([FromQuery] LoginRequest request)
        {
            var user = await userManager.GetSelfUserInfoAsync(request.Login, request.Password);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpGet("older-than/{age}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersOlderThan(int age)
        {
            var users = await userManager.GetUsersOlderThanAsync(age);
            return Ok(users);
        }

        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromQuery] string login, [FromQuery] bool soft = true)
        {
            var adminLogin = User.Identity?.Name;
            var result = await userManager.DeleteUserAsync(login, soft, adminLogin);
            return result ? Ok() : NotFound("User not found");
        }

        [HttpPost("RestoreUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RestoreUser([FromQuery] string login)
        {
            var result = await userManager.RestoreUserAsync(login);
            return result ? Ok() : NotFound("User not found");
        }
    }
}
