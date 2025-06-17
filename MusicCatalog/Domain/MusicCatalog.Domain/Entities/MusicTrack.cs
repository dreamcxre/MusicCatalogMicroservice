using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.Entities;
using System.Collections.Generic;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a music track in the catalog.
    /// </summary>
    public class MusicTrack : Entity<Guid>
    {
        #region Fields

        private readonly List<PlaybackHistory> _playbackHistories = [];

        #endregion

        #region Properties

        /// <summary>
        /// Gets the title of the music track.
        /// </summary>
        public Title Title { get; private set; }

        /// <summary>
        /// Gets the description or additional info about the track.
        /// </summary>
        public Description Description { get; private set; }

        /// <summary>
        /// Gets the duration of the track.
        /// </summary>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Gets the release date of the track.
        /// </summary>
        public DateTime ReleaseDate { get; private set; }

        /// <summary>
        /// Gets the genre of the music track.
        /// </summary>
        public Genre Genre { get; private set; }

        /// <summary>
        /// Gets the artist who created this track.
        /// </summary>
        public Artist Artist { get; private set; }

        /// <summary>
        /// Gets the album this track belongs to (if any).
        /// </summary>
        public Album? Album { get; private set; }

        /// <summary>
        /// Gets the current status of the track (e.g., Published, Draft, Removed).
        /// </summary>
        public TrackStatus Status { get; private set; }

        /// <summary>
        /// Gets whether the track is currently available for playback.
        /// </summary>
        public bool IsAvailable => Status == TrackStatus.Published;

        /// <summary>
        /// Gets the number of times the track has been played.
        /// </summary>
        public int PlayCount => _playbackHistories.Count;

        /// <summary>
        /// Gets the read-only collection of playback histories.
        /// </summary>
        public IReadOnlyCollection<PlaybackHistory> PlaybackHistories => _playbackHistories.AsReadOnly();

        #endregion

        #region Constructors

        protected MusicTrack()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicTrack"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the track.</param>
        /// <param name="title">The title of the track.</param>
        /// <param name="description">Description or additional info.</param>
        /// <param name="duration">The duration of the track.</param>
        /// <param name="releaseDate">The release date of the track.</param>
        /// <param name="genre">The genre of the track.</param>
        /// <param name="artist">The artist who created the track.</param>
        /// <param name="album">The album this track belongs to (optional).</param>
        /// <param name="status">The current status of the track.</param>
        public MusicTrack(
            Guid id,
            Title title,
            Description description,
            TimeSpan duration,
            DateTime releaseDate,
            Genre genre,
            Artist artist,
            Album? album,
            TrackStatus status) : base(id)
        {
            if (duration <= TimeSpan.Zero)
                throw new InvalidTrackDurationException(this, duration);

            if (releaseDate > DateTime.UtcNow)
                throw new FutureReleaseDateException(this, releaseDate);

            Title = title ?? throw new ArgumentNullValueException(nameof(title));
            Description = description ?? throw new ArgumentNullValueException(nameof(description));
            Duration = duration;
            ReleaseDate = releaseDate;
            Genre = genre;
            Artist = artist ?? throw new ArgumentNullValueException(nameof(artist));
            Album = album;
            Status = status;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a playback history entry when the track is played.
        /// </summary>
        /// <param name="user">The user who played the track.</param>
        /// <returns>true if playback was successfully recorded; otherwise false.</returns>
        public bool RecordPlay(User user)
        {
            if (!IsAvailable)
                return false;

            var historyEntry = PlaybackHistory.Create(user, this); // ✔️ Через фабричный метод
            _playbackHistories.Add(historyEntry);
            return true;
        }

        /// <summary>
        /// Updates the track's metadata.
        /// </summary>
        /// <param name="admin">The admin updating the track.</param>
        /// <param name="newTitle">New title (optional).</param>
        /// <param name="newDescription">New description (optional).</param>
        /// <param name="newDuration">New duration (optional).</param>
        /// <param name="newGenre">New genre (optional).</param>
        /// <param name="newAlbum">New album (optional).</param>
        public void UpdateMetadata(Admin admin,
        Title? newTitle = null,
        Description? newDescription = null,
        TimeSpan? newDuration = null,
        Genre? newGenre = null,
        Album? newAlbum = null)
        {
            if (newDuration.HasValue && newDuration <= TimeSpan.Zero)
                throw new InvalidTrackDurationException(this, newDuration.Value);

            if (newTitle != null) Title = newTitle;
            if (newDescription != null) Description = newDescription;
            if (newDuration.HasValue) Duration = newDuration.Value;
            if (newGenre != null) Genre = newGenre;
            if (newAlbum != null) Album = newAlbum;
        }

        /// <summary>
        /// Sets the track status (e.g., published, removed).
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