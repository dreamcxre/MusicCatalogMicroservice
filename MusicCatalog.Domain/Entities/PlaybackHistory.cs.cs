using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a playback history entry for a music track.
    /// </summary>
    public class PlaybackHistory : Entity<Guid>
    {
        #region Properties

        /// <summary>
        /// Gets the date and time when the track was played.
        /// </summary>
        public DateTime PlayedAt { get; }

        /// <summary>
        /// Gets the user who played the track.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Gets the music track that was played.
        /// </summary>
        public MusicTrack Track { get; }

        #endregion

        #region Constructor

        protected PlaybackHistory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybackHistory"/> class.
        /// </summary>
        /// <param name="user">The user who played the track.</param>
        /// <param name="track">The music track that was played.</param>
        /// <param name="playedAt">The date and time when the track was played.</param>
        public PlaybackHistory(User user, MusicTrack track, DateTime playedAt)
            : this(Guid.NewGuid(), user, track, playedAt)
        {
        }

        public static PlaybackHistory Create(User user, MusicTrack track)
        {
            return new PlaybackHistory(Guid.NewGuid(), user, track, DateTime.UtcNow);
        }

        protected PlaybackHistory(Guid id, User user, MusicTrack track, DateTime playedAt)
            : base(id)
        {
            if (track == null)
                throw new ArgumentNullValueException(nameof(track));

            if (user == null)
                throw new ArgumentNullValueException(nameof(user));

            if (playedAt > DateTime.UtcNow)
                throw new InvalidPlaybackTimeException(track, playedAt);

            if (!track.IsAvailable)
                throw new PlaybackUnavailableTrackException(track);

            User = user;
            Track = track;
            PlayedAt = playedAt;
        }

        #endregion
    }
}