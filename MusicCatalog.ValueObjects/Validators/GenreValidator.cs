using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Exceptions;

namespace MusicCatalog.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Validates genre value object.
    /// </summary>
    public class GenreValidator : IValidator<string>
    {
        public static int MAX_LENGTH => 50;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), ExceptionMessages.GENRE_NOT_NULL_OR_WHITE_SPACE);

            if (value.Length > MAX_LENGTH)
                throw new TitleLongValueException(value, MAX_LENGTH);
        }
    }
}