using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace IdentityService.WebAPI.Controllers.Login;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IdDomainService _idService;
    private readonly IIdRepository _repository;

    public LoginController(IdDomainService idService, IIdRepository repository)
    {
        _idService = idService;
        _repository = repository;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateWorld()
    {
        if (await _repository.FindByNameAsync("admin") != null)
        {
            return StatusCode((int)HttpStatusCode.Conflict, "已经初始化过了");
        }

        User user = new User("admin");
        IdentityResult r = await _repository.CreateAsync(user, "dotnet9");
        Debug.Assert(r.Succeeded);
        string token = await _repository.GenerateChangePhoneNumberTokenAsync(user, "18699999999");
        SignInResult cr = await _repository.ChangePhoneNumberAsync(user.Id, "17999999999", token);
        Debug.Assert(cr.Succeeded);
        r = await _repository.AddToRoleAsync(user, "User");
        Debug.Assert(r.Succeeded);
        r = await _repository.AddToRoleAsync(user, "Admin");
        Debug.Assert(r.Succeeded);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserResponse>> GetUserInfo()
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        User? user = await _repository.FindByIdAsync(Guid.Parse(userId!));
        if (user == null)
        {
            return NotFound();
        }

        return new UserResponse(user.Id, user.PhoneNumber, user.CreationTime);
    }
}