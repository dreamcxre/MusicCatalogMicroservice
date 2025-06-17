using MusicCatalog.Domain.ValueObjects.Base;
using System;

namespace MusicCatalog.Domain.ValueObjects.Validators
{
    public class DurationValidator : IValidator<TimeSpan>
    {
        public void Validate(TimeSpan value)
        {
            if (value <= TimeSpan.Zero)
                throw new ArgumentException("Track duration must be greater than zero.", nameof(value));
        }
    }
}