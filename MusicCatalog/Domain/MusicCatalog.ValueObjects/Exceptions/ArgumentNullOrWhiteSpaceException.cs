namespace MusicCatalog.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// The exception that is thrown when one of the string arguments is null, empty or consists only of white-space characters.
    /// </summary>
    internal class ArgumentNullOrWhiteSpaceException(string paramName, string message)
        : ArgumentNullException(paramName, message)
    {
    }
}