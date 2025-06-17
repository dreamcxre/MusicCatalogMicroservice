using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Exceptions;

namespace MusicCatalog.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Validates description value object.
    /// </summary>
    public class DescriptionValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), ExceptionMessages.DESCRIPTION_NOT_NULL_OR_WHITE_SPACE);
        }
    }
}