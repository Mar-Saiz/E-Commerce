using E_Commerce.Data.Enums;

namespace E_Commerce.Data.DTOs.User
{
    public class CreateUserDto
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Roles Role { get; set; }
        public required bool IsActive { get; set; }
    }
}
