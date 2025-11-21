using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Seeds
{
    public class DefaultAdminUser
    {
        public async static Task SeedAsync(UserManager<AppUser> UserManager)
        {
            AppUser user = new()
            {
                FirstName = "WIL",
                LastName = "Caba",
                Cedula = "000001",
                Email = "Admin@gmail.com",
                Phone = "8098578140",
                Status = true,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                UserName = "Admin"
            };

            if (await UserManager.Users.AllAsync(u => u.Id != user.Id))
            {

                var entityUser = await UserManager.FindByEmailAsync(user.Email);
                if (entityUser == null)
                {
                  await UserManager.CreateAsync(user, "123Pas$$word!");
                    await UserManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
