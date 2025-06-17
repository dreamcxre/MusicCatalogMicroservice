using MusicCatalog.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MusicCatalog.Domain.Repositories.Abstractions
{
    public interface IAdminRepository : IRepository<Admin, Guid>
    {
        Task<Admin?> GetAdminByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}