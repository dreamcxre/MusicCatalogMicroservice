using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.Enums;
using System.Collections.Generic;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents an administrator of the music streaming service.
    /// </summary>
    public class Admin(Guid id, Username username) : Entity<Guid>(id)
    {
        #region Fields

        private readonly List<MusicTrack> _addedTracks = [];

        #endregion

        #region Properties

        /// <summary>
        /// Gets the admin's username.
        /// </summary>
        public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));

        /// <summary>
        /// Gets a read-only collection of tracks added by this admin.
        /// </summary>
        public IReadOnlyCollection<MusicTrack> AddedTracks => _addedTracks.AsReadOnly();

        #endregion

        #region Methods

        /// <summary>
        /// Changes the admin's username.
        /// </summary>
        /// <param name="newUsername">The new username.</param>
        /// <returns>true if successfully changed; otherwise false.</returns>
        public bool ChangeUsername(Username newUsername)
        {
            if (Username == newUsername) return false;
            Username = newUsername;
            return true;
        }

        /// <summary>
        /// Adds a new track to the music catalog.
        /// </summary>
        /// <param name="title">Title of the track.</param>
        /// <param name="description">Description or additional info.</param>
        /// <param name="duration">Duration of the track.</param>
        /// <param name="releaseDate">Release date of the track.</param>
        /// <param name="genre">Genre of the track.</param>
        /// <param name="artist">Artist who created the track.</param>
        /// <param name="album">Album the track belongs to (optional).</param>
        /// <returns>The newly created music track.</returns>
        public MusicTrack AddTrack(
            Title title,
            Description description,
            TimeSpan duration,
            DateTime releaseDate,
            Genre genre,
            Artist artist,
            Album? album = null)
        {
            var track = new MusicTrack(
                Guid.NewGuid(),
                title,
                description,
                duration,
                releaseDate,
                genre,
                artist,
                album,
                TrackStatus.Published);

            _addedTracks.Add(track);
            return track;
        }

        /// <summary>
        /// Removes a track from the catalog.
        /// </summary>
        /// <param name="track">The track to remove.</param>
        /// <returns>true if successfully removed; otherwise false.</returns>
        public bool RemoveTrack(MusicTrack track)
        {
            if (!_addedTracks.Contains(track))
                return false;

            track.SetStatus(this, TrackStatus.Removed);
            return true;
        }

        #endregion
    }
}