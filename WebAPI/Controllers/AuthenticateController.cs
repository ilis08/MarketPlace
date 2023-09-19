using ApplicationService.Contracts;
using Azure.Core;
using Data.Entitites;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : HomeController
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole<long>> roleManager;
    private readonly IConfiguration configuration;
    private readonly ITokenService tokenService;

    public AuthenticateController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole<long>> _roleManager, IConfiguration _configuration, ITokenService _tokenService)
    {
        userManager = _userManager;
        roleManager = _roleManager;
        configuration = _configuration;
        tokenService = _tokenService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return BadRequest("Bad credentials");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);

        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }

        var userRoleIds = await userManager.GetRolesAsync(user);
        var roles = new List<IdentityRole<long>>();

        foreach (var id in userRoleIds)
        {
            var role = await roleManager.FindByNameAsync(id);
            roles.Add(role);
        }

        var accessToken = tokenService.CreateToken(user, roles);

        user.RefreshToken = configuration.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

        await userManager.UpdateAsync(user);

        return Ok(new LoginResponse
        {
            Username = user.UserName!,
            Email = user.Email!,
            Token = accessToken,
            RefreshToken = user.RefreshToken
        });
    }

    [HttpPost]
    [Route("register-customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        var userExists = await userManager.Users.AnyAsync(x => x.UserName == request.Email);

        if (userExists)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        ApplicationUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await userManager.CreateAsync(user, request.Password);

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        if (!result.Succeeded) return BadRequest(request);

        var findUser = await userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (findUser == null) return NotFound($"User {request.Email} not found");

        if (!await roleManager.RoleExistsAsync(UserRoles.Customer))
            await roleManager.CreateAsync(new IdentityRole<long>(UserRoles.Customer));

        var roleResult = await userManager.AddToRoleAsync(findUser, UserRoles.Customer);

        if (!roleResult.Succeeded)
        {
            // if adding the user to the role failed, delete the user
            await userManager.DeleteAsync(findUser);
            return BadRequest("Failed to assign role to user.");
        }

        return await Login(new LoginRequest
        {
            Email = findUser.Email,
            Password = request.Password
        });
    }

    /*[HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        var userExists = await userManager.Users.AnyAsync(x => x.UserName == model.Username);
        if (userExists)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await userManager.AddToRoleAsync(user, UserRoles.User);
        }
        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }*/
}
