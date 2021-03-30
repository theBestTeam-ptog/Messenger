using JetBrains.Annotations;

namespace Domain.Mappers
{
    public interface IDuplexMapper<T1, T2>
    {
        [CanBeNull]
        T1 Map([CanBeNull] T2 source);
        
        [CanBeNull]
        T2 Map([CanBeNull] T1 source);
    }
}