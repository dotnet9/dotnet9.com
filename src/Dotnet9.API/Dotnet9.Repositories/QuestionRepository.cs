using Dotnet9.EntityFramework;
using Dotnet9.IRepositories;
using Dotnet9.Models.Models;
using Dotnet9.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(Dotnet9Context context) : base(context)
    {
    }

    public async Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbContext().Questions
            .Where(x => x.Id == id)
            .Include(x => x.QuestionComments)
            .ThenInclude(x => x.CreateUser)
            .SingleOrDefaultAsync(cancellationToken);
    }
}