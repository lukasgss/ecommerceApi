using AutoMapper;
using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces;
using ecommerceApi.Application.Common.Interfaces.Authentication;
using ecommerceApi.Application.Common.Interfaces.Persistence;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IMapper mapper, IPasswordService passwordService, IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticationResult> RegisterAsync(RegisterRequest request)
    {
        if (await _userRepository.GetUserByEmailAsync(request.Email) is not null)
        {
            throw new ConflictException("User with given email already exists.");
        }

        var hashedPassword = _passwordService.HashPassword(request.Password);
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = hashedPassword,
        };

        _userRepository.Add(user);
        await _unitOfWork.CommitAsync();

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

        return new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token);
    }

    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user is null || !_passwordService.ComparePassword(password, user.Password))
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