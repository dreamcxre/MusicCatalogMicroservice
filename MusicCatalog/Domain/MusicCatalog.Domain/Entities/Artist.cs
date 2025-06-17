using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.ValueObjects;
using System.Collections.Generic;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a music artist in the catalog.
    /// </summary>
    public class Artist(Guid id, Username username) : Entity<Guid>(id)
    {
        #region Fields

        private readonly List<MusicTrack> _tracks = [];
        private readonly List<Album> _albums = [];

        #endregion

        #region Properties

        /// <summary>
        /// Gets the username of the artist.
        /// </summary>
        public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));

        /// <summary>
        /// Gets the read-only collection of tracks created by this artist.
        /// </summary>
        public IReadOnlyCollection<MusicTrack> Tracks => _tracks.AsReadOnly();

        /// <summary>
        /// Gets the read-only collection of albums created by this artist.
        /// </summary>
        public IReadOnlyCollection<Album> Albums => _albums.AsReadOnly();

        #endregion

        #region Methods

        /// <summary>
        /// Changes the artist's username.
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
        /// Adds a track to the artist's discography.
        /// </summary>
        /// <param name="track">The track to add.</param>
        public void AddTrack(MusicTrack track)
        {
            if (!_tracks.Contains(track))
                _tracks.Add(track);
        }

        /// <summary>
        /// Adds an album to the artist's discography.
        /// </summary>
        /// <param name="album">The album to add.</param>
        public void AddAlbum(Album album)
        {
            if (!_albums.Contains(album))
                _albums.Add(album);
        }

        #endregion
    }
}