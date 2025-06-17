namespace MusicCatalog.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// Provides string constants for error messages used in value object validation.
    /// </summary>
    internal static class ExceptionMessages
    {
        public const string TITLE_NOT_NULL_OR_WHITE_SPACE = "The Title must not be null, empty or consist only of whitespace.";
        public const string DESCRIPTION_NOT_NULL_OR_WHITE_SPACE = "The Description must not be null, empty or consist only of whitespace.";
        public const string USERNAME_NOT_NULL_OR_WHITE_SPACE = "The Username must not be null, empty or consist only of whitespace.";
        public const string VALIDATOR_MUST_BE_SPECIFIED = "A validator must be specified for this type.";
        public const string GENRE_NOT_NULL_OR_WHITE_SPACE = "The Genre must not be null, empty or consist only of whitespace.";
        public const string INVALID_DURATION = "Track duration must be greater than zero.";
        public const string FUTURE_RELEASE_DATE = "Release date cannot be in the future.";
    }
}