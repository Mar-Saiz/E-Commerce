namespace E_Commerce.Data.DTOs.User
{
    public class ForgotPasswordRequestDto
    {
        public required string UserName { get; set; }
        public string? Origin { get; set; }
    }
}
