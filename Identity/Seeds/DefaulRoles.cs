using Microsoft.AspNetCore.Identity;

namespace Identity.Seeds
{
    public static class DefaulRoles
    {
        public async static Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Seller"));
            await roleManager.CreateAsync(new IdentityRole("Client"));

        }
    }
}
