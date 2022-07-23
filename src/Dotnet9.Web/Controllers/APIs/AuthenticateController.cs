using Dotnet9.Domain.Shared.UserRoles;

namespace Dotnet9.Web.Controllers.APIs;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IOptionsSnapshot<JwtSettings> _jwtSettings;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
        IOptionsSnapshot<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.UserName);
        if (user == null)
        {
            return NotFound($"用户名不存在{loginRequest.UserName}");
        }

        if (await _userManager.IsLockedOutAsync(user))
        {
            return BadRequest("用户已经被锁定");
        }

        var success = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!success)
        {
            await _userManager.AccessFailedAsync(user);
            return NotFound("密码不正确");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id)
        };
        authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = GetToken(authClaims);

        return Ok(new LoginResult
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        });
    }

    [HttpPost]
    public async Task<ActionResult<RegisterResult>> Register([FromBody] RegisterRequest registerRequest)
    {
        var userExists = await _userManager.FindByNameAsync(registerRequest.UserName);
        if (userExists != null)
        {
            return BadRequest(
                new RegisterResult
                    { Code = StatusCodes.Status500InternalServerError.ToString(), Message = "已经存在的用户名" });
        }

        var user = new IdentityUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(
                new RegisterResult
                    { Code = StatusCodes.Status500InternalServerError.ToString(), Message = "创建用户失败，请检查用户信息再尝试" });
        }

        return new RegisterResult { Code = StatusCodes.Status200OK.ToString(), Message = "创建用户成功!" };
    }

    [HttpPost]
    public async Task<ActionResult<RegisterResult>> RegisterAdmin([FromBody] RegisterRequest registerRequest)
    {
        var userExists = await _userManager.FindByNameAsync(registerRequest.UserName);
        if (userExists != null)
        {
            return BadRequest(
                new RegisterResult
                    { Code = StatusCodes.Status500InternalServerError.ToString(), Message = "已经存在的用户名" });
        }

        var user = new IdentityUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(
                new RegisterResult
                    { Code = StatusCodes.Status500InternalServerError.ToString(), Message = "创建用户失败，请检查用户信息再尝试" });
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }

        if (await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        return new RegisterResult { Code = StatusCodes.Status200OK.ToString(), Message = "创建用户成功!" };
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret!));

        var token = new JwtSecurityToken(
            _jwtSettings.Value.ValidIssuer,
            _jwtSettings.Value.ValidAudience,
            expires: DateTime.Now.AddMinutes(3),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            claims: authClaims
        );

        return token;
    }
}