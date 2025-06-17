using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.ValueObjects.Base;
using MusicCatalog.Domain.ValueObjects.Exceptions;

namespace MusicCatalog.Domain.ValueObjects.Validators
{
    public class ReleaseDateValidator : IValidator<DateTime>
    {
        public void Validate(DateTime value)
        {
            if (value > DateTime.UtcNow)
                throw new FutureReleaseDateException(null!, value);
        }
    }
}