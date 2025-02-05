using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FleetControl.Infrastructure.Persistence.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly FleetControlDbContext _context;
        private readonly DbSet<T> _dataSet;

        public GenericRepository(FleetControlDbContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        public async Task<T> Create(T item)
        {
            await _dataSet.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> Exists(int id)
        {
            return await _dataSet.AnyAsync(g => g.Equals(id) && !g.IsDeleted);
        }

        public async Task<List<T>> GetAll(bool includeNavigation = false, bool recursiveInclude = false)
        {
            IQueryable<T> query = _dataSet;

            if (includeNavigation)
            {
                query = ApplyIncludes(query, recursiveInclude);
            }

            return await query.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<T?> GetById(int id, bool includeNavigation = false, bool recursiveInclude = false)
        {
            IQueryable<T> query = _dataSet;

            if (includeNavigation)
            {
                query = ApplyIncludes(query, recursiveInclude);
            }

            return await query.Where(x => !x.IsDeleted).SingleOrDefaultAsync(x => x.Id.Equals(id) && !x.IsDeleted);
        }

        public async Task Update(T item)
        {
            _dataSet.Update(item);
            await _context.SaveChangesAsync();
        }

        private IQueryable<T> ApplyIncludes(IQueryable<T> query, bool recursiveInclude)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));

            if (entityType == null) return query;

            foreach (var navigation in entityType.GetNavigations())
            {
                query = query.Include(navigation.Name);

                if (recursiveInclude)
                {
                    query = ApplyThenIncludes(query, navigation);
                }
            }

            return query;
        }

        private IQueryable<T> ApplyThenIncludes(IQueryable<T> query, INavigation navigation)
        {
            var entityType = navigation.TargetEntityType;

            foreach (var nestedNavigation in entityType.GetNavigations())
            {
                query = query.Include($"{navigation.Name}.{nestedNavigation.Name}");
            }

            return query;
        }
    }
}
