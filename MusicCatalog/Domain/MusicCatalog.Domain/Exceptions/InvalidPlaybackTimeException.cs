using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Domain.Exceptions
{
    public class InvalidPlaybackTimeException(MusicTrack track, DateTime playedAt)
        : ArgumentException($"The playback time for the track '{track.Title}' cannot be in the future.")
    {
        public DateTime PlayedAt => playedAt;
        public MusicTrack Track => track;
    }
}