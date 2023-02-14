using System.ComponentModel.DataAnnotations;
using ecommerceApi.Application.Common.Interfaces.Authentication;
using ecommerceApi.Application.Services.Authentication;
using ecommerceApi.Application.Validation;
using Microsoft.AspNetCore.Mvc;
using ecommerceApi.Application.Services.FailedValidation;

namespace ecommerceApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResult>> Register(RegisterRequest request)
    {
        RegisterRequestValidator registerRequestValidator = new();
        var validationResult = registerRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = ValidationErrors.GenerateModelStateDictionary(validationResult);
            return ValidationProblem(modelStateDictionary);
        }

        var authResult = await _authenticationService.RegisterAsync(request);

        return new AuthenticationResult(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResult>> Login(LoginRequest request)
    {
        var response = await _authenticationService.LoginAsync(request.Email, request.Password);

        return Ok(response);
    }
}