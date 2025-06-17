using MusicCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicCatalog.Domain.Repositories.Abstractions
{
    public interface IMusicTrackRepository : IRepository<MusicTrack, Guid>
    {
        Task<IEnumerable<MusicTrack>> GetAllByReleaseDateAsync(
            DateTime releaseDate,
            CancellationToken cancellationToken,
            bool asNoTracking = false);
    }
}