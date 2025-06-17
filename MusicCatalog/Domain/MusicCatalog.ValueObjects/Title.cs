using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents the title of a music track or album.
    /// </summary>
    public class Title(string title)
        : ValueObject<string>(new TitleValidator(), title);
}