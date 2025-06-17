using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.Exceptions;
using System;
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
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public Genre Genre { get; private set; }
        public Artist Artist { get; private set; }
        public Album? Album { get; private set; }
        public TrackStatus Status { get; private set; }
        public bool IsAvailable => Status == TrackStatus.Published;
        public int PlayCount => _playbackHistories.Count;
        public IReadOnlyCollection<PlaybackHistory> PlaybackHistories => _playbackHistories.AsReadOnly();
        #endregion

        #region Constructors

        /// <summary>
        /// Protected constructor for EF Core
        /// </summary>
        protected MusicTrack() { }

        /// <summary>
        /// Internal constructor with full validation
        /// </summary>
        protected MusicTrack(
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
            ValidateParameters(duration, releaseDate, title, description, artist);

            Title = title;
            Description = description;
            Duration = duration;
            ReleaseDate = releaseDate;
            Genre = genre;
            Artist = artist;
            Album = album;
            Status = status;
        }

        /// <summary>
        /// Public constructor for application code (defaults to Published status)
        /// </summary>
        public MusicTrack(
            Guid id,
            Title title,
            Description description,
            TimeSpan duration,
            DateTime releaseDate,
            Genre genre,
            Artist artist,
            Album? album = null)
            : this(id, title, description, duration, releaseDate, genre, artist, album, TrackStatus.Published)
        {
        }

        #endregion

        #region Private Methods
        private void ValidateParameters(
            TimeSpan duration,
            DateTime releaseDate,
            Title title,
            Description description,
            Artist artist)
        {
            if (duration <= TimeSpan.Zero)
                throw new InvalidTrackDurationException(this, duration);

            if (releaseDate > DateTime.UtcNow)
                throw new FutureReleaseDateException(this, releaseDate);

            if (title == null)
                throw new ArgumentNullValueException(nameof(title));

            if (description == null)
                throw new ArgumentNullValueException(nameof(description));

            if (artist == null)
                throw new ArgumentNullValueException(nameof(artist));
        }
        #endregion

        #region Public Methods
        public bool RecordPlay(User user)
        {
            if (!IsAvailable) return false;

            _playbackHistories.Add(PlaybackHistory.Create(user, this));
            return true;
        }

        public void UpdateMetadata(
            Admin admin,
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

        public void SetStatus(Admin admin, TrackStatus newStatus)
        {
            Status = newStatus;
        }
        #endregion
    }
}