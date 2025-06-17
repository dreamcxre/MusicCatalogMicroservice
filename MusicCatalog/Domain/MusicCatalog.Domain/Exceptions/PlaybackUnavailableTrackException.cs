using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Domain.Exceptions
{
    public class PlaybackUnavailableTrackException(MusicTrack track)
        : InvalidOperationException($"The track '{track.Title}' is not available for playback.")
    {
        public MusicTrack Track => track;
    }
}