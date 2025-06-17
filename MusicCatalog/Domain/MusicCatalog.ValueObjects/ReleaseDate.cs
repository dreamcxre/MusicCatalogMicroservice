using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents the release date of a music track.
    /// </summary>
    public class ReleaseDate(DateTime date)
        : ValueObject<DateTime>(new ReleaseDateValidator(), date);
}