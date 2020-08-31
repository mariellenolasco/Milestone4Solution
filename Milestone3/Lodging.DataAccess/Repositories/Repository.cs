using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lodging.DataAccess.Repositories
{
    /// <summary>
    /// This class uses generics to create a general repo to perform data access methods on any class that is connected to the DB
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> where TEntity : class
    {
        public readonly DbSet<TEntity> _db;

        public Repository(LodgingContext context)
        {
            _db = context.Set<TEntity>();
        }

        public virtual async Task DeleteAsync(int id) => _db.Remove(await SelectAsync(id));

        public virtual async Task InsertAsync(TEntity entry) => await _db.AddAsync(entry).ConfigureAwait(true);

        public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await _db.ToListAsync();

        public virtual async Task<TEntity> SelectAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

        public virtual void Update(TEntity entry) => _db.Update(entry);
    }
}