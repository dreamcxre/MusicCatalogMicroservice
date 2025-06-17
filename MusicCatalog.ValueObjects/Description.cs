using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents a description of a music catalog entity (track, album, artist).
    /// </summary>
    public class Description(string description)
        : ValueObject<string>(new DescriptionValidator(), description);
}