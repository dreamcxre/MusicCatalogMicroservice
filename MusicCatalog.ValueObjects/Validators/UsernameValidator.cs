using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Exceptions;

namespace MusicCatalog.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Validates username value object.
    /// </summary>
    public class UsernameValidator : IValidator<string>
    {
        public static int MAX_LENGTH => 30;
        public static int MIN_LENGTH => 3;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), ExceptionMessages.USERNAME_NOT_NULL_OR_WHITE_SPACE);

            if (value.Length > MAX_LENGTH)
                throw new UsernameLongValueException(value, MAX_LENGTH);
        }
    }
}