using System.Linq.Expressions;

namespace EDI.Contracts.Generics
{
    public interface IGenericActionDbQuery<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<List<T>> GetByFilterAsync(string queryString);
    }
}
