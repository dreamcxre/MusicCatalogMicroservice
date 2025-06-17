using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.Enums;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents an album in the music catalog.
    /// </summary>
    public class Album(Guid id, Title title, Artist artist, DateTime releaseDate) : Entity<Guid>(id)
    {
        #region Properties

        /// <summary>
        /// Gets the title of the album.
        /// </summary>
        public Title Title { get; } = title;

        /// <summary>
        /// Gets the artist who created the album.
        /// </summary>
        public Artist Artist { get; } = artist;

        /// <summary>
        /// Gets the release date of the album.
        /// </summary>
        public DateTime ReleaseDate { get; } = releaseDate;

        /// <summary>
        /// Gets the current status of the album (e.g., Published, Draft, Removed).
        /// </summary>
        public TrackStatus Status { get; private set; } = TrackStatus.Published;

        /// <summary>
        /// Gets the read-only collection of tracks in the album.
        /// </summary>
        public IReadOnlyCollection<MusicTrack> Tracks => _tracks.AsReadOnly();
        private readonly List<MusicTrack> _tracks = [];

        #endregion

        #region Methods

        /// <summary>
        /// Adds a track to the album.
        /// </summary>
        /// <param name="track">The track to add.</param>
        public void AddTrack(MusicTrack track)
        {
            if (track.Artist != Artist)
                throw new InvalidOperationException("Track must belong to the same artist.");

            if (!_tracks.Contains(track))
                _tracks.Add(track);
        }

        /// <summary>
        /// Sets the status of the album.
        /// </summary>
        /// <param name="admin">The admin changing the status.</param>
        /// <param name="newStatus">The new status.</param>
        public void SetStatus(Admin admin, TrackStatus newStatus)
        {
            Status = newStatus;
        }

        #endregion
    }
}