using MusicCatalog.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MusicCatalog.Domain.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}