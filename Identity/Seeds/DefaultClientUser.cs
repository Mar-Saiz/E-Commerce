using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Seeds
{
    public class DefaultClientUser
    {
        public async static Task SeedAsync(
            UserManager<AppUser> userManager)
        {
            
            
            AppUser user = new()
            {
                FirstName = "Xavier",
                LastName = "Casilla",
                Cedula = "000003",
                Email = "xavier13yt@gmail.com",
                Phone = "8098578140",
                EmailConfirmed = true,
                Status = true,
                PhoneNumberConfirmed = true,
                UserName = "Xavi"
            };

            if (await userManager.Users.AllAsync(u => u.Id != user.Id))
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    var result = await userManager.CreateAsync(user, "123Pas$$word!");

                    await userManager.AddToRoleAsync(user, "Client");

                }
            }
        }
    }
}