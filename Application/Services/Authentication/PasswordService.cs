using ecommerceApi.Application.Common.Interfaces.Authentication;
using BCrypt.Net;

namespace ecommerceApi.Application.Services.Authentication;

public class PasswordService : IPasswordService
{
    public bool ComparePassword(string plainTextPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}