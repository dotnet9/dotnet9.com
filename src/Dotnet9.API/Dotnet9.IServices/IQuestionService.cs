using Dotnet9.IServices.Base;
using Dotnet9.Models.Models;

namespace Dotnet9.IServices;

public interface IQuestionService : IBaseService<Question>
{
    Task<Question> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Question> GetQuestionDetailsAsync(int id, CancellationToken cancellationToken = default);

    Task AddQuestionComment(int id, int userId, string content, CancellationToken cancellationToken = default);
}