using Dotnet9.IRepositories;
using Dotnet9.IServices;
using Dotnet9.Models.Models;
using Dotnet9.Services.Base;

namespace Dotnet9.Services;

public class QuestionService : BaseService<Question>, IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository) : base(
        questionRepository)
    {
        _questionRepository = questionRepository;
    }


    public async Task<Question> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _questionRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Question> GetQuestionDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _questionRepository.GetByIdAsync(id, cancellationToken);
        entity.Traffic += 1;

        await _questionRepository.UpdateAsync(entity, true, cancellationToken: cancellationToken);

        return entity;
    }

    public async Task AddQuestionComment(int id, int userId, string content,
        CancellationToken cancellationToken = default)
    {
        var entity = await _questionRepository.GetByIdAsync(id, cancellationToken);
        entity.QuestionComments!.Add(new QuestionComment()
        {
            Content = content,
            CreateTime = DateTime.Now,
            CreateUserId = userId
        });
        await _questionRepository.UpdateAsync(entity, true, cancellationToken);
    }
}