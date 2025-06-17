using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Domain.Exceptions
{
    public class InvalidTrackDurationException(MusicTrack track, TimeSpan duration)
        : ArgumentException($"The duration of the track '{track.Title}' must be greater than zero.")
    {
        public TimeSpan Duration => duration;
        public MusicTrack Track => track;
    }
}