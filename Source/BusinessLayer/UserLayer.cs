using Home.Models.DTOs;
using Home.Models.Entities.Other;
using Home.Source.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Home.Source.BusinessLayer
{
    public class UserLayer
    {
        private readonly AspNetRepository aspNetRepository;

        public UserLayer(AspNetRepository aspNetRepository)
        {
            this.aspNetRepository = aspNetRepository;
        }

        public async Task<IdentityResult> SignUpAsync(UserSignUpEditorDTO dto)
        {
            User user = new()
            {
                UserName = dto.Email,
                Email = dto.Email,
            };

            return await aspNetRepository.CreateAsync(user, dto.Password);
        }

        public async Task<SignInResult> LogInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await aspNetRepository.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure);
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await aspNetRepository.FindByEmailAsync(email);
        }

        public async Task AddToRoleAsync(User? user, List<string> roles)
        {
            foreach (var role in roles)
            {
                await aspNetRepository.AddToRoleAsync(user!, role);
            }
        }

        public async Task<IList<string>> GetUserRolesAsync(User? user)
        {
            return await aspNetRepository.GetUserRolesAsync(user!);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await aspNetRepository.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<List<string>> GetAppRolesAsync()
        {
            List<string> roles = new List<string>();
            var result = await aspNetRepository.GetAppRolesAsync();

            foreach (var item in result)
            {
                roles.Add(item.Name!);
            }

            return roles;
        }
    }
}
