using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.Enums;
using System.Collections.Generic;
using System;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents an album in the music catalog.
    /// </summary>
    public class Album : Entity<Guid>
    {
        #region Fields
        private readonly List<MusicTrack> _tracks = [];
        #endregion
        protected Album() { }

        // Основной конструктор (может быть public или internal)
        public Album(Guid id, Title title, DateTime releaseDate) : base(id)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            ReleaseDate = releaseDate;
        }

        #region Properties
        /// <summary>
        /// Gets the title of the album.
        /// </summary>
        public Title Title { get; private set; }

        /// <summary>
        /// Gets the artist who created the album.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets the release date of the album.
        /// </summary>
        public DateTime ReleaseDate { get; private set; }

        /// <summary>
        /// Gets the current status of the album.
        /// </summary>
        public TrackStatus Status { get; private set; } = TrackStatus.Published;

        /// <summary>
        /// Gets the tracks in the album.
        /// </summary>
        public IReadOnlyCollection<MusicTrack> Tracks => _tracks.AsReadOnly();
        #endregion

        #region Constructors



        /// <summary>
        /// Internal constructor with full validation.
        /// </summary>
        protected Album(
            Guid id,
            Title title,
            DateTime releaseDate,
            TrackStatus status) : base(id)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            ReleaseDate = releaseDate;
            Status = status;
        }

        /// <summary>
        /// Public constructor for application code.
        /// </summary>
        public Album(
            Guid id,
            Title title,
            Artist artist,
            DateTime releaseDate)
            : this(id, title, releaseDate, TrackStatus.Published)
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Adds a track to the album.
        /// </summary>
        public void AddTrack(MusicTrack track)
        {
            if (track.Artist != Artist)
                throw new InvalidOperationException("Track must belong to the same artist.");

            if (!_tracks.Contains(track))
                _tracks.Add(track);
        }

        /// <summary>
        /// Sets the album status.
        /// </summary>
        public void SetStatus(Admin admin, TrackStatus newStatus)
        {
            Status = newStatus;
        }
        #endregion
    }
}