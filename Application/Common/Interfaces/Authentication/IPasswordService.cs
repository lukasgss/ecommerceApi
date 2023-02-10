namespace ecommerceApi.Application.Common.Interfaces.Authentication;

public interface IPasswordService
{
    string HashPassword(string password);
    bool ComparePassword(string plainTextpassword, string hashedPassword);
}