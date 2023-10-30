using Home.Models.DTOs;
using Home.Source.BusinessLayer;
using Home.Source.Common;
using Home.Source.Extensions;
using Home.Source.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Home.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserLayer userLayer;
        private readonly IConfiguration configuration;

        public UserController(
            UserLayer userLayer, 
            IConfiguration configuration)
        {
            this.userLayer = userLayer;
            this.configuration = configuration;
        }

        [HttpPost(template: "signup")]
        public async Task<ActionResult<UserTokenDTO>> SignUp([FromBody] UserSignUpEditorDTO dto)
        {
            var result = await userLayer.SignUpAsync(dto);
            if (result.Succeeded)
            {
                var user = await userLayer.FindByEmailAsync(dto.Email);
                var roles = await GetInitRolesAsync();
                await userLayer.AddToRoleAsync(user, roles);
                var claims = TokenHelper.CreateClaims(user?.Id, user?.Email, roles);
                var token = TokenHelper.BuildToken(claims, configuration["JWT:Key"]!);
                return token;
            }

            return BadRequest(result?.Errors.Select(p => p.Description).ToList()); ;
        }

        [HttpPost(template: "login")]
        public async Task<ActionResult<UserTokenDTO>> LogIn([FromBody] UserLogInEditorDTO dto)
        {
            var result = await userLayer.LogInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await userLayer.FindByEmailAsync(dto.Email);
                var roles = await userLayer.GetUserRolesAsync(user);
                var claims = TokenHelper.CreateClaims(user?.Id, user?.Email, roles.ToList());
                var token = TokenHelper.BuildToken(claims, configuration["JWT:Key"]!);

                return token;
            }

            return BadRequest(new List<string>() { AppResponse.WrongCredentials.GetBasicDescription() });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(template: "changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordEditorDTO dto)
        {
            var user = await userLayer.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email)!);
            if (user == null)
            {
                return BadRequest(new List<string>() { AppResponse.UserNotFound.GetBasicDescription() });
            }

            var result = await userLayer.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result?.Errors.Select(p => p.Description).ToList());
            }
        }

        [HttpGet(template: "isEmailAvailable/{email}")]
        public async Task<ActionResult> IsEmailAvailable(string email)
        {
            var user = await userLayer.FindByEmailAsync(email);

            bool isAvailable = user == null;
            return Ok(isAvailable);
        }

        private async Task<List<string>> GetInitRolesAsync()
        {
            var isAdmin = false;
            var roles = await userLayer.GetAppRolesAsync(); ;

            if (isAdmin)
            {
                roles = roles.Where(p => p != AppRole.PROFILE_USER.GetBasicName()).ToList();
            }
            else
            {
                roles = roles.Where(p => p != AppRole.PROFILE_ADMIN.GetBasicName()).ToList();
            }
            
            return roles;
        }
    }
}
