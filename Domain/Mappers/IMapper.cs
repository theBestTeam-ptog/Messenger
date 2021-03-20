namespace Domain.Mappers
{
    public interface IMapper<in TSource, out TResult>
    {
        TResult Map(TSource source);
    }
}