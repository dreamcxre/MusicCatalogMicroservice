namespace MusicCatalog.Domain.ValueObjects.Exceptions
{
    internal class TitleShortValueException(string title, int minLength)
        : FormatException($"Title '{title}' is shorter than minimum allowed length of {minLength} characters.")
    {
        public string Title => title;
        public int MinLength => minLength;
    }
}