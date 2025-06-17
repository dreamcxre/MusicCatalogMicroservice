using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Domain.Exceptions
{
    public class FutureReleaseDateException(MusicTrack track, DateTime releaseDate)
        : ArgumentException($"The release date for the track '{track.Title}' cannot be in the future.")
    {
        public DateTime ReleaseDate => releaseDate;
        public MusicTrack Track => track;
    }
}