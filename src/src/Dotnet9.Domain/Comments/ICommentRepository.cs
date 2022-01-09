using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Comments;

public interface ICommentRepository : IRepository<Comment, Guid>
{
    Task<List<Comment>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
}