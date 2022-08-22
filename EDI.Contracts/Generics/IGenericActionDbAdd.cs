
namespace EDI.Contracts.Generics
{
    public interface IGenericActionDbAdd<T> where T : class
    {
        Task<Tuple<T, bool>> AddAsync(T entity);
    }
}
