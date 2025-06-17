namespace MusicCatalog.Domain.ValueObjects.Exceptions
{
    internal class TitleLongValueException(string title, int maxLength)
        : FormatException($"Title '{title}' exceeds maximum allowed length of {maxLength} characters.")
    {
        public string Title => title;
        public int MaxLength => maxLength;
    }
}