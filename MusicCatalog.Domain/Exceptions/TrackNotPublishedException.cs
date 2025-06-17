using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Domain.Exceptions
{
    public class TrackNotPublishedException(MusicTrack track)
        : InvalidOperationException($"The track '{track.Title}' is not published and cannot be used.")
    {
        public MusicTrack Track => track;
    }
}