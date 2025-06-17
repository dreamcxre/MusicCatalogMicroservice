using MusicCatalog.Domain.Entities.Base;
using MusicCatalog.Domain.ValueObjects;
using System.Collections.Generic;

namespace MusicCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a user playlist in the music streaming service.
    /// </summary>
    public class Playlist : Entity<Guid>
    {
        #region Fields
        private readonly List<MusicTrack> _tracks = [];
        #endregion

        #region Properties
        /// <summary>
        /// Gets the owner of the playlist.
        /// </summary>
        public User Owner { get; private set; }

        /// <summary>
        /// Gets or sets the name of the playlist.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Indicates whether the playlist is public or private.
        /// </summary>
        public bool IsPublic { get; private set; } = true;

        /// <summary>
        /// Gets the tracks in the playlist.
        /// </summary>
        public IReadOnlyCollection<MusicTrack> Tracks => _tracks.AsReadOnly();
        #endregion

        #region Constructors

        /// <summary>
        /// Protected constructor for EF Core.
        /// </summary>
        protected Playlist()
        {
        }

        /// <summary>
        /// Internal constructor with full validation.
        /// </summary>
        protected Playlist(
            Guid id,
            User owner,
            string name,
            bool isPublic) : base(id)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsPublic = isPublic;
        }

        /// <summary>
        /// Public constructor for application code (defaults to public visibility).
        /// </summary>
        public Playlist(
            Guid id,
            User owner,
            string name)
            : this(id, owner, name, isPublic: true)
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Adds a track to the playlist.
        /// </summary>
        public void AddTrack(MusicTrack track)
        {
            if (track == null)
                throw new ArgumentNullException(nameof(track));

            if (!_tracks.Contains(track))
                _tracks.Add(track);
        }

        /// <summary>
        /// Removes a track from the playlist.
        /// </summary>
        public void RemoveTrack(MusicTrack track)
        {
            if (track == null)
                throw new ArgumentNullException(nameof(track));

            _tracks.Remove(track);
        }

        /// <summary>
        /// Renames the playlist.
        /// </summary>
        public void Rename(string newName)
        {
            Name = newName ?? throw new ArgumentNullException(nameof(newName));
        }

        /// <summary>
        /// Sets the visibility of the playlist.
        /// </summary>
        public void SetVisibility(bool isPublic)
        {
            IsPublic = isPublic;
        }
        #endregion
    }
}