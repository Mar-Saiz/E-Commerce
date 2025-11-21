using E_Commerce.Data.DTOs.User;
using Identity.Services;

namespace E_Commerce.Data.Interfaces
{
    public interface IBaseAccountService
    {
        public Task<RegisterResponseDto> RegisterUser(SaveUserDto saveDto, string? origin, bool? isApi = false);
        public Task<EditResponseDto> EditUser(SaveUserDto saveDto, string? origin, bool? isCreated = false, bool? isApi = false);
        public Task<UserResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request, bool? isApi = false);
        public Task<UserResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request);
        public Task<UserResponseDto> DeleteAsync(string id);
        public Task<UserDto?> GetUserById(string Id);
        public Task<List<UserDto>> GetAllUserByRole(string role);
        public Task<UserResponseDto> ConfirmAccountAsync(string userId, string token);
        public Task<UserResponseDto> ChangeStatusAsync(string userId, bool status);
    }
}
