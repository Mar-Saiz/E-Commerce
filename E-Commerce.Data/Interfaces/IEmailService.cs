using E_Commerce.Data.DTOs;
using E_Commerce.Data.DTOs.Email;

namespace E_Commerce.Data.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto emailRequestDto);
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
