namespace Payments.API.Mappers;

public interface IMapper<TIn, TOut>
{
    TOut Map(
        TIn input);
}