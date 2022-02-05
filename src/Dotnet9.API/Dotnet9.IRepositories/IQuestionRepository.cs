using Dotnet9.IRepositories.Base;
using Dotnet9.Models.Models;

namespace Dotnet9.IRepositories;

public interface IQuestionRepository : IBaseRepository<Question>
{
    Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}