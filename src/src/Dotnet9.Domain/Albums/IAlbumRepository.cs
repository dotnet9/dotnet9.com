using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Albums;

public interface IAlbumRepository : IRepository<Album, Guid>
{
    Task<Album> FindByNameAsync(string name);

    Task<List<Album>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
}