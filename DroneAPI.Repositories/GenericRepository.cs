using DroneAPI.Core;
using DroneAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace DroneAPI.Repositories
{
    /// <summary>
    /// The Drone API Generic Repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IGenericRepository&lt;T&gt;" />
    public class DroneAPIRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// The context
        /// </summary>
        protected readonly DroneAPIDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneAPIRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public DroneAPIRepository(DroneAPIDbContext context, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            var user = httpContextAccessor.HttpContext.User?.Identity?.Name;
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedBy = user ?? "User info";

            context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public async Task UpdateAsync(int id, T entity)
        {
            var _entity = await context.Set<T>().FindAsync(id);
            var _user = httpContextAccessor.HttpContext.User?.Identity?.Name;

            entity.CreatedBy = _entity.CreatedBy;
            entity.CreatedDateTime = _entity.CreatedDateTime;
            entity.ModifiedDateTime = DateTime.Now;
            entity.ModifiedBy = _user ?? "User info";

            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void AddRange(IEnumerable<T> entities)
        {
            var user = httpContextAccessor.HttpContext.User?.Identity?.Name;

            foreach (var entity in entities)
            {
                entity.CreatedDateTime = DateTime.Now;
                entity.CreatedBy = user ?? "User info";
                context.Set<T>().Add(entity);
            }
            //context.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, string includeProperties = "")
        {
            var query = context.Set<T>().Where(expression);
            foreach (var includeProperty in includeProperties.Split
               (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets all asynchronously.
        /// </summary>
        /// <returns>IEnumerable<T></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
            => await context.Set<T>().ToListAsync();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T</returns>
        public async Task<T> GetByIdAsync(int id)
            => await context.Set<T>().FindAsync(id);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(T entity)
           => context.Set<T>().Remove(entity);

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void RemoveRange(IEnumerable<T> entities)
          => context.Set<T>().RemoveRange(entities);
    }
}
