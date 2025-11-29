using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Identity.Seeds
{
    public class DefaultSellerUser
    {
        public async static Task SeedAsync(UserManager<AppUser> UserManager)
        {
            AppUser user = new()
            {
                FirstName = "Agentes",
                LastName = "TuCaja",
                Cedula = "000002",
                Email = "Cashier@gmail.com",
                Phone = "8098578140",
                EmailConfirmed = true,
                Status = true,
                PhoneNumberConfirmed = true,
                UserName = "Seller"
            };

            if (await UserManager.Users.AllAsync(u => u.Id != user.Id))
            {

                var entityUser = await UserManager.FindByEmailAsync(user.Email);
                if (entityUser == null)
                {
                    await UserManager.CreateAsync(user, "123Pas$$word!");
                    await UserManager.AddToRoleAsync(user, "Seller");
                }

            }

        }
    }
}

