using Home.Models.Entities.Other;
using Home.Source.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Home.Source.Repositories
{
    public class AspNetRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AspNetRepository(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task CreateRolesAsync()
        {
            var databaseRoles = roleManager.Roles.ToList();
            var appRoles = EnumHelper.GetAppRoleNames();

            var roleNamesToAdd = appRoles.Except(databaseRoles.Select(p => p.Name));
            foreach (var role in roleNamesToAdd)
            {
                await roleManager.CreateAsync(new IdentityRole(role!));
            }
        }

        public async Task DeleteRolesAsync()
        {
            var databaseRoles = await roleManager.Roles.ToListAsync();
            var appRoles = EnumHelper.GetAppRoleNames();

            var roleNamesToDelete = databaseRoles.Select(p => p.Name).Except(appRoles);
            var rolesToDelete = databaseRoles.Where(p => roleNamesToDelete.Contains(p.Name));
            foreach (var role in rolesToDelete)
            {
                await roleManager.DeleteAsync(role);
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task AddToRoleAsync(User? user, string role)
        {
            await userManager.AddToRoleAsync(user!, role);
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure);
        }

        public async Task<IList<string>> GetUserRolesAsync(User? user)
        {
            return await userManager.GetRolesAsync(user!);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<List<IdentityRole>> GetAppRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }
    }
}
