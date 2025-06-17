using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Exceptions;

namespace MusicCatalog.Domain.ValueObjects.Validators
{
    public class DurationValidator : IValidator<TimeSpan>
    {
        public void Validate(TimeSpan value)
        {
            if (value <= TimeSpan.Zero)
                throw new InvalidTrackDurationException(null!, value); 
        }
    }
}