using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents a genre of a music track.
    /// </summary>
    public class Genre(string name)
        : ValueObject<string>(new GenreValidator(), name);
}