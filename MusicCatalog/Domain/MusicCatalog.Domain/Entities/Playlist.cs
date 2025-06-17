using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a user playlist in the music streaming service.
    /// </summary>
    public class Playlist(Guid id, User owner, string name) : Entity<Guid>(id)
    {
        #region Properties

        /// <summary>
        /// Gets the owner of the playlist.
        /// </summary>
        public User Owner { get; } = owner;

        /// <summary>
        /// Gets or sets the name of the playlist.
        /// </summary>
        public string Name { get; private set; } = name;

        /// <summary>
        /// Indicates whether the playlist is public or private.
        /// </summary>
        public bool IsPublic { get; private set; } = true;

        /// <summary>
        /// Gets the read-only collection of tracks in the playlist.
        /// </summary>
        public IReadOnlyCollection<MusicTrack> Tracks => _tracks.AsReadOnly();
        private readonly List<MusicTrack> _tracks = [];

        #endregion

        #region Methods

        /// <summary>
        /// Adds a track to the playlist.
        /// </summary>
        /// <param name="track">The track to add.</param>
        public void AddTrack(MusicTrack track)
        {
            if (!_tracks.Contains(track))
                _tracks.Add(track);
        }

        /// <summary>
        /// Removes a track from the playlist.
        /// </summary>
        /// <param name="track">The track to remove.</param>
        public void RemoveTrack(MusicTrack track)
        {
            _tracks.Remove(track);
        }

        /// <summary>
        /// Renames the playlist.
        /// </summary>
        /// <param name="newName">The new name.</param>
        public void Rename(string newName)
        {
            Name = newName;
        }

        /// <summary>
        /// Sets the visibility of the playlist.
        /// </summary>
        /// <param name="isPublic">true if public; false if private.</param>
        public void SetVisibility(bool isPublic)
        {
            IsPublic = isPublic;
        }

        #endregion
    }
}