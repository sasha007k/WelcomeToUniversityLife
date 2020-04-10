using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public static class DataInitializer
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager,
            DatabaseContext context)
        {
            await SeedRoles(roleManager);
            await SeedSiteAdmin(userManager);
        }

        public static async Task SeedSiteAdmin(UserManager<User> userManager)
        {
            var email = "siteadmin@gmail.com";
            var password = "12345";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new User();
                user.Email = email;
                user.UserName = email;

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded) await userManager.AddToRoleAsync(user, "SiteAdmin");
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roleNames = {"User", "UniversityAdmin", "SiteAdmin"};
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (roleExist == false) roleResult = await roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }
    }
}