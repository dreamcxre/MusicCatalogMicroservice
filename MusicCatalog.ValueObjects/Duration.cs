using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents the duration of a music track.
    /// </summary>
    public class Duration(TimeSpan timeSpan)
        : ValueObject<TimeSpan>(new DurationValidator(), timeSpan);
}