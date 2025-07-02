using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToDoApi.DTOs;
using ToDoApi.Interfaces;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IAuditService _auditService;
        
        public AccountController(UserManager<IdentityUser> userManager, IAuthService authService, IAuditService auditService)
        {
            _userManager = userManager;
            _authService = authService;
            _auditService = auditService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDto)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser();
                user.UserName = signUpDto.UserName;
                user.Email = signUpDto.Email;
                var result = await _userManager.CreateAsync(user, signUpDto.Password);
                if (result.Succeeded)
                {
                    JwtSecurityToken token = await _authService.CreateTokenAsync(user);
                    var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                    await _auditService.LogActionAsync(user.Id,
                        "SignUp",
                        $" {user.UserName} has Signed Up ",
                        ip);
                    
                    return Ok(new {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                return BadRequest(new
                {
                    message = "Invalid username or password.",
                    errors = result.Errors
                });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginDto.Username);
                if (user != null)
                {
                    var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                    if (result)
                    {
                        JwtSecurityToken token = await _authService.CreateTokenAsync(user);
                        
                        var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                        await _auditService.LogActionAsync(user.Id,
                            "Login",
                            $" {user.UserName} has Logged In ",
                            ip);
                        
                        return Ok(new {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
                ModelState.AddModelError(String.Empty, "Invalid username or password.");
            }
            return BadRequest(ModelState);
        }
    }
}
