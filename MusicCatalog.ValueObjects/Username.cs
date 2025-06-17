using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents a username of a user or admin in the music streaming service.
    /// </summary>
    public class Username(string name)
        : ValueObject<string>(new UsernameValidator(), name);
}