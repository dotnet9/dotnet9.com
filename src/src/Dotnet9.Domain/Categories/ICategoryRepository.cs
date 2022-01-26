using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Categories;

public interface ICategoryRepository : IRepository<Category, Guid>
{
    Task<Category> FindByNameAsync(string name);

    Task<List<Category>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);

    Task<List<CategoryCount>> GetListCountAsync();
}