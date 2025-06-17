using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.Exceptions;
using System.Collections.Generic;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a user of the music streaming service.
    /// </summary>
    public class User(Guid id, Username username) : Entity<Guid>(id)
    {
        #region Fields

        private readonly List<MusicTrack> _favoriteTracks = [];
        private readonly List<Playlist> _playlists = [];

        #endregion

        #region Properties

        /// <summary>
        /// Gets the username of the user.
        /// </summary>
        public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));

        /// <summary>
        /// Gets the read-only collection of favorite tracks.
        /// </summary>
        public IReadOnlyCollection<MusicTrack> FavoriteTracks => _favoriteTracks.AsReadOnly();

        /// <summary>
        /// Gets the read-only collection of user's playlists.
        /// </summary>
        public IReadOnlyCollection<Playlist> Playlists => _playlists.AsReadOnly();

        #endregion

        #region Methods

        /// <summary>
        /// Changes the username of the user.
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
        /// Adds a track to the user's favorites.
        /// </summary>
        /// <param name="track">The track to add to favorites.</param>
        /// <returns>true if successfully added; otherwise false.</returns>
        public bool AddToFavorites(MusicTrack track)
        {
            if (!_favoriteTracks.Contains(track))
            {
                _favoriteTracks.Add(track);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a track from the user's favorites.
        /// </summary>
        /// <param name="track">The track to remove from favorites.</param>
        /// <returns>true if successfully removed; otherwise false.</returns>
        public bool RemoveFromFavorites(MusicTrack track)
        {
            return _favoriteTracks.Remove(track);
        }

        /// <summary>
        /// Creates a new playlist for the user.
        /// </summary>
        /// <param name="name">The name of the playlist.</param>
        /// <returns>The created playlist.</returns>
        public Playlist CreatePlaylist(string name)
        {
            var playlist = new Playlist(Guid.NewGuid(), this, name);
            _playlists.Add(playlist);
            return playlist;
        }

        /// <summary>
        /// Records that the user played the given track.
        /// </summary>
        /// <param name="track">The track played by the user.</param>
        /// <returns>true if playback was recorded; otherwise false.</returns>
        public bool RecordPlayback(MusicTrack track)
        {
            if (!track.IsAvailable)
                return false;

            var historyEntry = new PlaybackHistory(this, track, DateTime.UtcNow);
            return true;
        }

        #endregion
    }
}