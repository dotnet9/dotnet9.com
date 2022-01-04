using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Abouts;

public interface IAboutRepository : IRepository<About, Guid>
{
    Task<About> GetAsync();
}