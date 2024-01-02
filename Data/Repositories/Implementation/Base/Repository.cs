//+---------------------------------------------------------------------------------------------------+
//|                                        Repository.cs                                              |
//|                                    ======================                                         |
//+---------------------------------------------------------------------------------------------------+
//| This is a C# implementation of a generic repository class that follows the repository pattern.    |
//| The purpose of the class is to provide a set of CRUD (Create, Read, Update, Delete) operations    |
//| that can be used to interact with a database table in a consistent and reusable way.              |
//|                                                                                                   |
//| The class is parameterized with a type parameter T that is constrained to be a reference type     |
//| (class). This allows the class to work with any entity type that is mapped to the underlying      |
//| database using Entity Framework.                                                                  |
//|                                                                                                   |
//| The class implements the IRepository<T> interface, which defines the contract for the             |
//| repository operations. The interface includes methods for inserting, updating, deleting, and      |
//| querying entities, as well as methods for counting and enumerating entities.                      |
//|                                                                                                   |
//| The Repository<T> class also implements the IDisposable interface, which allows the               |
//| class to release any unmanaged resources it is holding when it is no longer needed. This is       |
//| important for ensuring that database connections are properly closed and that any other resources |
//| associated with the repository are cleaned up.                                                    |
//|                                                                                                   |
//| Overall, this class provides a simple and consistent way to interact with a database using the    |
//| repository pattern, which can help to improve code quality and maintainability.                   |
//+---------------------------------------------------------------------------------------------------+

using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Context;
using PharmaStore.Data.Repositories.Interface.Base;

namespace PharmaStore.Data.Repositories.Implementation.Base
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        protected readonly AppDbContext _context;
        private bool _disposed = false;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public int Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int InsertRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return _context.SaveChanges();
        }

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public int DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public List<T> List()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<List<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T? Find(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T?> FindAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetEnumerable()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
