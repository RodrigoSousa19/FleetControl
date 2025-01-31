using FleetControl.Core.Entities;

namespace FleetControl.Core.Interfaces.Generic
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> Create(T item);
        Task<T?> GetById(int id, bool includeNavigation = false, bool recursiveSearch = false);
        Task<List<T>> GetAll(bool includeNavigation = false, bool recursiveSearch = false);
        Task Update(T item);
        Task<bool> Exists(int id);
    }
}
