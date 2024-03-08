using MyVilla_API.Models;
using System.Linq.Expressions;

namespace MyVilla_API.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}
