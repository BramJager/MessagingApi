using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingApi.Business.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task Update(TEntity entity);
        Task Add(TEntity entity);
        Task Delete(TEntity entity);
        Task Save();

    }
}
