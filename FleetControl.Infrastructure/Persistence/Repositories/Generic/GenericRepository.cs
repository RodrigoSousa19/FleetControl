using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace FleetControl.Infrastructure.Persistence.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly FleetControlDbContext _context;
        private readonly DbSet<T> _dataSet;
        private readonly HashSet<string> _includedPaths = new();
        private readonly Dictionary<Type, List<string>> _navigationCache = new();

        private const int MAX_DEPTH_RECURSIVE_INCLUDE = 3;

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
            if (!_navigationCache.TryGetValue(typeof(T), out var navigations))
            {
                navigations = _context.Model.FindEntityType(typeof(T))?
                    .GetNavigations()
                    .Select(n => n.Name)
                    .ToList() ?? new List<string>();

                _navigationCache[typeof(T)] = navigations;
            }

            foreach (var navigationPath in navigations)
            {
                if (_includedPaths.Add(navigationPath))
                {
                    query = query.Include(navigationPath);

                    if (recursiveInclude)
                    {
                        query = ApplyThenIncludes(query, navigationPath, 1);
                    }
                }
            }

            return query;
        }

        private IQueryable<T> ApplyThenIncludes(IQueryable<T> query, string parentPath, int currentDepth)
        {
            if (currentDepth >= MAX_DEPTH_RECURSIVE_INCLUDE) return query;

            var parentType = GetTypeFromPath(typeof(T), parentPath);
            if (parentType == null) return query;

            if (!_navigationCache.TryGetValue(parentType, out var nestedNavigations))
            {
                nestedNavigations = _context.Model.FindEntityType(parentType)?
                    .GetNavigations()
                    .Select(n => n.Name)
                    .ToList() ?? [];

                _navigationCache[parentType] = nestedNavigations;
            }

            foreach (var nestedNavigation in nestedNavigations)
            {
                var nestedPath = $"{parentPath}.{nestedNavigation}";

                if (_includedPaths.Add(nestedPath))
                {
                    query = query.Include(nestedPath);
                    query = ApplyThenIncludes(query, nestedPath, currentDepth + 1);
                }
            }

            return query;
        }

        private Type? GetTypeFromPath(Type rootType, string path)
        {
            var properties = path.Split('.');
            var currentType = rootType;

            foreach (var propName in properties)
            {
                var propInfo = currentType.GetProperty(propName);
                if (propInfo == null) return null;

                currentType = propInfo.PropertyType;
            }

            return currentType;
        }
    }
}
