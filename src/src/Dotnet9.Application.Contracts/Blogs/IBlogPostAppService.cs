using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dotnet9.Blogs;

public interface IBlogPostAppService : IApplicationService
{
    Task<BlogPostDto> GetAsync(Guid id);

    Task<PagedResultDto<BlogPostDto>> GetListAsync(GetBlogPostListDto input);

    Task<BlogPostDto> CreateAsync(CreateBlogPostDto input);

    Task UpdateAsync(Guid id, UpdateBlogPostDto input);

    Task DeleteAsync(Guid id);
}