namespace Identity.Services
{
    public class EditResponseDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public List<string>? Roles { get; set; }
        public bool IsVerified { get; set; }
        public bool HasError { get; set; }
        public required List<string> Errors { get; set; }
    }
}