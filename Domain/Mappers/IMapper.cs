using JetBrains.Annotations;

namespace Domain.Mappers
{
    public interface IMapper<in TSource, out TResult>
    {
        [CanBeNull]
        TResult Map([CanBeNull] TSource source);
    }
}