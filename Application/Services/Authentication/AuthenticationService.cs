using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces;
using ecommerceApi.Application.Common.Interfaces.Authentication;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        if (await _userRepository.GetUserByEmailAsync(email) is not null)
        {
            throw new ConflictException("User with given email already exists.");
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        await _userRepository.AddAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, firstName, lastName);

        return new AuthenticationResult(
            user.Id,
            firstName,
            lastName,
            email,
            token);
    }

    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user is null || user.Password != password)
        {
            throw new UnauthorizedException("Invalid credentials.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

        return new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token);
    }
}