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