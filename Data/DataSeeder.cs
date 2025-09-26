using DisasterRelief.Models;
using Microsoft.AspNetCore.Identity;

namespace DisasterRelief.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Roles
            var roles = new[] { "Admin", "Volunteer" };
            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole(r));
            }


            // Admin user
            var adminEmail = "admin@localdev";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, "Admin@1234");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}