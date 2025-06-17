using MusicCatalog.Domain.ValueObjects.Base;

namespace MusicCatalog.Domain.ValueObjects.Validators
{
    public class ReleaseDateValidator : IValidator<DateTime>
    {
        public void Validate(DateTime value)
        {
            if (value > DateTime.UtcNow)
                throw new ArgumentException("Release date cannot be in the future.", nameof(value));
        }
    }
}