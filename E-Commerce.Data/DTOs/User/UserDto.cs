namespace E_Commerce.Data.DTOs.User
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public bool? isVerified { get; set; }
        public required string Role { get; set; }
        public required bool IsActive { get; set; }
    }
}
