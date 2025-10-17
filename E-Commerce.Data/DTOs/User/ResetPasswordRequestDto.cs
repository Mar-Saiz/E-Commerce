﻿namespace E_Commerce.Data.DTOs.User
{
    public class ResetPasswordRequestDto
    {
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public required string Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}