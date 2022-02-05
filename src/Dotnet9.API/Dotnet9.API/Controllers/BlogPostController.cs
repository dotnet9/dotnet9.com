using Dotnet9.IServices;
using Dotnet9.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _service;

        public BlogPostController(IBlogPostService service)
        {
            _service = service;
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<List<BlogPost>> GetAsync(int id)
        {
            return await _service.GetListAsync(x => x.Id == id);
        }
    }
}