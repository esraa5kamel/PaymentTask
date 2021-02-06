using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAssignment.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> AddAsync(T entity);

        void Update(T entity);

    }


   
}
