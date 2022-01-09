using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dotnet9.Comments;

public interface ICommentAppService : IApplicationService
{
    Task<CommentDto> GetAsync(Guid id);

    Task<PagedResultDto<CommentDto>> GetListAsync(GetCommentListDto input);

    Task<CommentDto> CreateAsync(CreateCommentDto input);

    Task UpdateAsync(Guid id, UpdateCommentDto input);

    Task DeleteAsync(Guid id);
}