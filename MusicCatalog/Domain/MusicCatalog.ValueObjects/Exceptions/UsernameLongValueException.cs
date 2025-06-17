namespace MusicCatalog.Domain.ValueObjects.Exceptions
{
    internal class UsernameLongValueException(string username, int maxLength)
        : FormatException($"Username '{username}' exceeds maximum allowed length of {maxLength} characters.")
    {
        public string Username => username;
        public int MaxLength => maxLength;
    }
}