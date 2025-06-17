using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Exceptions;

namespace MusicCatalog.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Validates title value object.
    /// </summary>
    public class TitleValidator : IValidator<string>
    {
        public static int MIN_LENGTH => 3;
        public static int MAX_LENGTH => 100;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), ExceptionMessages.TITLE_NOT_NULL_OR_WHITE_SPACE);

            if (value.Length > MAX_LENGTH)
                throw new TitleLongValueException(value, MAX_LENGTH);

            if (value.Length < MIN_LENGTH)
                throw new TitleShortValueException(value, MIN_LENGTH);
        }
    }
}