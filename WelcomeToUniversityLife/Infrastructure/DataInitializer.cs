using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DataInitializer
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, DatabaseContext context)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context);
            await SeedSiteAdmin(userManager);
        }

        public static async Task SeedSiteAdmin(UserManager<User> userManager)
        {
            string email = "siteadmin@gmail.com";
            string password = "12345";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                User user = new User();
                user.Email = email;
                user.UserName = email;

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "SiteAdmin");
                }
            }
        }

        public static async Task SeedUsers(UserManager<User> userManager, DatabaseContext context)
        {
            string email = "siteadmin@gmail.com";
            string password = "siteadmin";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                User user = new User();
                user.Email = email;
                user.UserName = email;

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    await userManager.AddToRoleAsync(user, "UniversityAdmin");
                    await userManager.AddToRoleAsync(user, "SiteAdmin");
                }
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roleNames = { "User", "UniversityAdmin", "SiteAdmin" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (roleExist == false)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
        }
    }
}
