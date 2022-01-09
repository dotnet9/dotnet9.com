using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Comments;

public class CommentAppService : Dotnet9AppService, ICommentAppService
{
    private readonly ICommentRepository _commentRepository;

    public CommentAppService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto> GetAsync(Guid id)
    {
        var comment = await _commentRepository.GetAsync(id);
        return ObjectMapper.Map<Comment, CommentDto>(comment);
    }

    public async Task<PagedResultDto<CommentDto>> GetListAsync(GetCommentListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Comment.Text);
        }

        var comments = await _commentRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting,
            input.Filter);

        var totalCount = input.Filter == null
            ? await _commentRepository.CountAsync()
            : await _commentRepository.CountAsync(comment => comment.Text.Contains(input.Filter));

        return new PagedResultDto<CommentDto>(totalCount,
            ObjectMapper.Map<List<Comment>, List<CommentDto>>(comments));
    }

    [Authorize(Dotnet9Permissions.Comments.Create)]
    public async Task<CommentDto> CreateAsync(CreateCommentDto input)
    {
        var comment = ObjectMapper.Map<CreateCommentDto, Comment>(input);

        await _commentRepository.InsertAsync(comment);

        return ObjectMapper.Map<Comment, CommentDto>(comment);
    }

    [Authorize(Dotnet9Permissions.Comments.Edit)]
    public async Task UpdateAsync(Guid id, UpdateCommentDto input)
    {
        var comment = await _commentRepository.GetAsync(id);

        comment.Text = input.Text;
        comment.RepliedCommentId = input.RepliedCommentId;

        await _commentRepository.UpdateAsync(comment);
    }

    [Authorize(Dotnet9Permissions.Comments.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}